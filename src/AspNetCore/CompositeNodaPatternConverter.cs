﻿using NodaTime.Serialization.SystemTextJson;
using NodaTime.Text;
using System;
using System.Text.Json;

namespace Rocket.Surgery.LaunchPad.AspNetCore
{
    /// <summary>
    /// A JSON converter for types which can be represented by a single string value, parsed or formatted
    /// from an <see cref="IPattern{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type to convert to/from JSON.</typeparam>
    public sealed class CompositeNodaPatternConverter<T> : NodaConverterBase<T>
    {
        private readonly IPattern<T>[] _patterns;
        private readonly Action<T>? _validator;

        /// <summary>
        /// Creates a new instance with a pattern and no validator.
        /// </summary>
        /// <param name="patterns">The patterns to use for parsing and formatting.</param>
        public CompositeNodaPatternConverter(params IPattern<T>[] patterns) : this(null, patterns) { }

        /// <summary>
        /// Creates a new instance with a pattern and an optional validator. The validator will be called before each
        /// value is written, and may throw an exception to indicate that the value cannot be serialized.
        /// </summary>
        /// <param name="patterns">The patterns to use for parsing and formatting.</param>
        /// <param name="validator">The validator to call before writing values. May be null, indicating that no validation is required.</param>
        public CompositeNodaPatternConverter(Action<T>? validator, params IPattern<T>[] patterns)
        {
            this._patterns = patterns;
            this._validator = validator;
        }

        protected override T ReadJsonImpl(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            string text = reader!.GetString()!;

            ParseResult<T> result = null!;
            foreach (var patter in _patterns)
            {
                result = patter.Parse(text);
                if (result.Success)
                    break;
            }

            return result.Value;
        }

        protected override void WriteJsonImpl(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            _validator?.Invoke(value);
            writer.WriteStringValue(_patterns[0].Format(value));
        }
    }
}