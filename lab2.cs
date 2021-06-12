using System;
using Xunit;
using IIG.BinaryFlag;
namespace BinaryFlag.UnitTests
{
public class BinaryFlagTestLength
{
ulong minimum = 2;
ulong maximum = 17179868704;
[Fact]
public void LengthLessMin()
{
Assert.Throws<ArgumentOutOfRangeException>(() => {
MultipleBinaryFlag test = new
MultipleBinaryFlag(minimum - 1);
});
}
[Fact]
public void LengthMin()
{
MultipleBinaryFlag test = new
MultipleBinaryFlag(minimum);
Assert.True(test is MultipleBinaryFlag);
}
[Fact]
public void LengthMoreMin()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(minimum
+ 1);
Assert.True(test is MultipleBinaryFlag);
}
[Fact]
public void LengthLessMax()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(maximum
- 1);Assert.True(test is MultipleBinaryFlag);
}
[Fact]
public void LengthMax()
{
MultipleBinaryFlag test = new
MultipleBinaryFlag(maximum);
Assert.True(test is MultipleBinaryFlag);
}
[Fact]
public void LengthMoreMax()
{
Assert.Throws<ArgumentOutOfRangeException>(() => {
MultipleBinaryFlag test = new
MultipleBinaryFlag(maximum + 1);
});
}
}
public class BinaryFlagTestInitialisation
{
[Fact]
public void IntitialiseWithoutBool()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(3);
Assert.True(test.GetFlag());
}
[Fact]
public void InitialiseTrue()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(3,
true);
Assert.True(test.GetFlag());
}
[Fact]
public void InitialiseFalse()
{MultipleBinaryFlag test = new MultipleBinaryFlag(3,
false);
Assert.False(test.GetFlag());
}
}
public class BinaryFlagTestSetFlag
{
[Fact]
public void SetAllFlags()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(10,
false);
for(ulong i = 0; i < 10; i++) test.SetFlag(i);
Assert.True(test.GetFlag());
}
[Fact]
public void SetSomeFlags()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(10,
false);
for (ulong i = 0; i < 8; i++) test.SetFlag(i);
Assert.False(test.GetFlag());
}
[Fact]
public void SetFlagOutOfBound()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(3,
false);
Assert.Throws<ArgumentOutOfRangeException>(() => {
test.SetFlag(10);
});
}
}
public class BinaryFlagTestResetFlag
{
[Fact]
public void ResetAllFlags()
{MultipleBinaryFlag test = new MultipleBinaryFlag(10,
true);
for (ulong i = 0; i < 10; i++) test.ResetFlag(i);
Assert.False(test.GetFlag());
}
[Fact]
public void ResetSomeFlags()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(10,
true);
for (ulong i = 0; i < 8; i++) test.ResetFlag(i);
Assert.False(test.GetFlag());
}
[Fact]
public void ResetFlagOutOfBound()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(3,
true);
Assert.Throws<ArgumentOutOfRangeException>(() => {
test.ResetFlag(10);
});
}
}
public class BinaryFlagTestDisposeFlag
{
[Fact]
public void Dispose()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(2);
testBinaryFlag.Dispose();
Assert.Null(testBinaryFlag.GetFlag());
}
[Fact]
public void DisposeAndSet()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5);testBinaryFlag.Dispose();
for (ulong i = 0; i < 5; i++) testBinaryFlag.SetFlag(i);
Assert.Null(testBinaryFlag.GetFlag());
}
[Fact]
public void DisposeAndReset()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5);
testBinaryFlag.Dispose();
for(ulong i = 0; i < 5; i++)
testBinaryFlag.ResetFlag(i);
Assert.Null(testBinaryFlag.GetFlag());
}
}
public class BinarFlagTestMultipleChangesOfFlags
{
[Fact]
public void SetFlagTwice()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(10,
false);
for (ulong i = 0; i < 10; i++) test.SetFlag(i);
for (ulong i = 0; i < 10; i++) test.SetFlag(i);
Assert.True(test.GetFlag());
}
[Fact]
public void ResetFlagTwice()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(10,
true);
for (ulong i = 0; i < 10; i++) test.ResetFlag(i);
for (ulong i = 0; i < 10; i++) test.ResetFlag(i);
Assert.False(test.GetFlag());
}
[Fact]
public void SetAndResetFlag()
{MultipleBinaryFlag test = new MultipleBinaryFlag(10,
true);
for (ulong i = 0; i < 10; i++) test.SetFlag(i);
for (ulong i = 0; i < 10; i++) test.ResetFlag(i);
Assert.False(test.GetFlag());
}
[Fact]
public void ResetAndSetFlag()
{
MultipleBinaryFlag test = new MultipleBinaryFlag(10,
true);
for (ulong i = 0; i < 10; i++) test.ResetFlag(i);
for (ulong i = 0; i < 10; i++) test.SetFlag(i);
Assert.True(test.GetFlag());
}
}
}
