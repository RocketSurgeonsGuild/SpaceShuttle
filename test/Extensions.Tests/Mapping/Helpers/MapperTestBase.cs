﻿namespace Extensions.Tests.Mapping.Helpers;

public abstract class MapperTestBase(ITestOutputHelper testOutputHelper) : AutoFakeTest<XUnitTestContext>(XUnitDefaults.CreateTestContext(testOutputHelper))
{
    protected SettingsTask VerifyMethod(MethodResult result, object mapper, params object[] instances)
    {
        return Verify(result.Map(mapper, instances)).UseParameters(result.ToString()).HashParameters();
    }

    protected SettingsTask VerifyEachMethod(MethodResult result, object mapper, params object[] instances)
    {
        return Verify(result.MapEach(mapper, instances)).UseParameters(result.ToString()).HashParameters();
    }
}
