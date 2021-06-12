using System;
using Xunit;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using IIG.BinaryFlag;
namespace TestIntegral
{
public class TestInputLoginPassword
{
private const string Server =
@"DESKTOP-4S0QUR5\MSSQLSERVERLAB4";
private const string Database = @"IIG.CoSWE.AuthDB";
private const bool IsTrusted = true;
private const string Login = @"coswe";
private const string Password = @"L}EjpfCgru9X@GLj";
private const int ConnectionTimeout = 75;
readonly AuthDatabaseUtils db = new(Server, Database,
IsTrusted, Login, Password, ConnectionTimeout);
string empty = "";
string ASCII = "latinText";
string bytes2 = "ЇЄЎ";
string bytes3 = "你会翻译这个吗";
string bytes4 = "𒃅𒃆𒃇";
string emptypass = PasswordHasher.GetHash("");
string ASCIIpass = PasswordHasher.GetHash("latin0Text");
string bytes2pass = PasswordHasher.GetHash("ЇЄЎ");
string bytes3pass = PasswordHasher.GetHash("你会翻译这个吗");
string bytes4pass = PasswordHasher.GetHash("𒃅𒃆𒃇");
[Fact]
public void TestASCII()
{
db.AddCredentials(ASCII, ASCIIpass);
Assert.True(db.CheckCredentials(ASCII, ASCIIpass));
db.DeleteCredentials(ASCII, ASCIIpass);}
[Fact]
public void Test2Bytes()
{
db.AddCredentials(bytes2, bytes2pass);
Assert.True(db.CheckCredentials(bytes2, bytes2pass));
db.DeleteCredentials(bytes2, bytes2pass);
}
[Fact]
public void Test3Bytes()
{
db.AddCredentials(bytes3, bytes3pass);
Assert.True(db.CheckCredentials(bytes3, bytes3pass));
db.DeleteCredentials(bytes3, bytes3pass);
}
[Fact]
public void Test4Bytes()
{
db.AddCredentials(bytes4, bytes4pass);
Assert.True(db.CheckCredentials(bytes4, bytes4pass));
db.DeleteCredentials(bytes4, bytes4pass);
}
[Fact]
public void TestEmpty()
{
db.AddCredentials(empty, emptypass);
Assert.False(db.CheckCredentials(empty, emptypass));
db.DeleteCredentials(empty, emptypass);
}
[Fact]
public void TestIdempotency()
{
db.AddCredentials(ASCII, ASCIIpass);
Assert.False(db.AddCredentials(ASCII, ASCIIpass));
Assert.True(db.CheckCredentials(ASCII, ASCIIpass));
db.DeleteCredentials(ASCII, ASCIIpass);
}[Fact]
public void TestUpdateLogin()
{
db.AddCredentials(ASCII, ASCIIpass);
db.UpdateCredentials(ASCII, ASCIIpass, bytes3,
ASCIIpass);
Assert.False(db.CheckCredentials(ASCII, ASCIIpass));
Assert.True(db.CheckCredentials(bytes3, ASCIIpass));
db.DeleteCredentials(bytes3, ASCIIpass);
}
[Fact]
public void TestUpdatePassword()
{
db.AddCredentials(ASCII, ASCIIpass);
db.UpdateCredentials(ASCII, ASCIIpass, ASCII,
bytes2pass);
Assert.False(db.CheckCredentials(ASCII, ASCIIpass));
Assert.True(db.CheckCredentials(ASCII, bytes2pass));
db.DeleteCredentials(ASCII, bytes2pass);
}
[Fact]
public void TestUpdateInputs()
{
db.AddCredentials(ASCII, ASCIIpass);
db.UpdateCredentials(ASCII, ASCIIpass, bytes3,
bytes3pass);
Assert.False(db.CheckCredentials(ASCII, ASCIIpass));
Assert.True(db.CheckCredentials(bytes3, bytes3pass));
db.DeleteCredentials(bytes3, bytes3pass);
}
[Fact]
public void TestUpdateInvalidInputs()
{
db.AddCredentials(bytes3, bytes3pass);
db.UpdateCredentials(ASCII, ASCIIpass, bytes2,
bytes2pass);
Assert.False(db.CheckCredentials(ASCII, ASCIIpass));
Assert.False(db.CheckCredentials(bytes2, bytes2pass));Assert.True(db.CheckCredentials(bytes3, bytes3pass));
db.DeleteCredentials(bytes3, bytes3pass);
}
}
public class TestBinaryFlag
{
string path = "C:/Users/User/file.txt";
[Fact]
public void TestInitTrue()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, true);
bool? flag = testBinaryFlag.GetFlag();
Assert.True(flag);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("True", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestInitFalse()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, false);
bool? flag = testBinaryFlag.GetFlag();
Assert.False(flag);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("False", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestSetAll()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, false);
bool? flag = testBinaryFlag.GetFlag();
Assert.False(flag);
for (uint i = 0; i < 5; i++) testBinaryFlag.SetFlag(i);BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("True", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestResetAll()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, true);
bool? flag = testBinaryFlag.GetFlag();
Assert.True(flag);
for (uint i = 0; i < 5; i++)
testBinaryFlag.ResetFlag(i);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("False", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestSetSome()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, false);
bool? flag = testBinaryFlag.GetFlag();
Assert.False(flag);
for (uint i = 0; i < 3; i++) testBinaryFlag.SetFlag(i);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("False", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestResetSome()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, true);
bool? flag = testBinaryFlag.GetFlag();
Assert.True(flag);
for (uint i = 0; i < 3; i++)
testBinaryFlag.ResetFlag(i);BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("False", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestDispose()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, true);
bool? flag = testBinaryFlag.GetFlag();
Assert.True(flag);
testBinaryFlag.Dispose();
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("", BaseFileWorker.ReadAll(path));
Assert.Null(testBinaryFlag.GetFlag());
}
[Fact]
public void TestDisposeSet()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, false);
bool? flag = testBinaryFlag.GetFlag();
Assert.False(flag);
testBinaryFlag.Dispose();
for (uint i = 0; i < 5; i++) testBinaryFlag.SetFlag(i);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestDisposeReset()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, true);
bool? flag = testBinaryFlag.GetFlag();
Assert.True(flag);
testBinaryFlag.Dispose();for (uint i = 0; i < 5; i++)
testBinaryFlag.ResetFlag(i);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestDisposeAssignFalse()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, true);
bool? flag = testBinaryFlag.GetFlag();
Assert.True(flag);
testBinaryFlag.Dispose();
testBinaryFlag = new MultipleBinaryFlag(5, false);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("False", BaseFileWorker.ReadAll(path));
}
[Fact]
public void TestDisposeAssignTrue()
{
MultipleBinaryFlag testBinaryFlag = new
MultipleBinaryFlag(5, false);
bool? flag = testBinaryFlag.GetFlag();
Assert.False(flag);
testBinaryFlag.Dispose();
testBinaryFlag = new MultipleBinaryFlag(5, true);
BaseFileWorker.Write(testBinaryFlag.GetFlag().ToString(), path);
Assert.Equal("True", BaseFileWorker.ReadAll(path));
}
}
}
