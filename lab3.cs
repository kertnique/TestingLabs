using System;
using System.Reflection;
using System.Text;
using Xunit;
using IIG.PasswordHashingUtils;
namespace TestPasswordHashingUtils
{
public class TestInit
{
PasswordHasher hasher = new PasswordHasher();
string saltASCII = "salt";
string saltASCII2 = "salt2";
string saltEmpty = "";
string salt2Bytes = "‫;"كوفي‬
string salt3Bytes = "汉字";
string salt4Bytes = "𠃐"; // this character is undisplayable in git
uint positiveAdler = 1;
uint positiveAdler2 = 2;
uint negativeAdler = 0;
static Type hasherType = typeof(PasswordHasher);
FieldInfo currSalt = hasherType.GetField("_salt",
BindingFlags.Static | BindingFlags.NonPublic);
FieldInfo currAdler = hasherType.GetField("_modAdler32",
BindingFlags.Static | BindingFlags.NonPublic);
[Fact]
public void Route0157Null()
{
PasswordHasher.Init(saltASCII, positiveAdler);
PasswordHasher.Init(null, positiveAdler2);
Assert.Equal(saltASCII,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler2,
currAdler.GetValue(hasher));
}
[Fact]
public void Route0157EmptyStr(){
PasswordHasher.Init(saltASCII, positiveAdler);
PasswordHasher.Init(saltEmpty, positiveAdler2);
Assert.Equal(saltASCII,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler2,
currAdler.GetValue(hasher));
}
[Fact]
public void Route0167Null()
{
PasswordHasher.Init(saltASCII, positiveAdler);
PasswordHasher.Init(null, negativeAdler);
Assert.Equal(saltASCII,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler, currAdler.GetValue(hasher));
}
[Fact]
public void Route0167EmptyStr()
{
PasswordHasher.Init(saltASCII, positiveAdler);
PasswordHasher.Init(saltEmpty, negativeAdler);
Assert.Equal(saltASCII,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler, currAdler.GetValue(hasher));
}
[Fact]
public void Route02457()
{
PasswordHasher.Init(saltASCII, positiveAdler);
PasswordHasher.Init(saltASCII2, positiveAdler2);
Assert.Equal(saltASCII2,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler2,
currAdler.GetValue(hasher));
}
[Fact]
public void Route023457_2Bytes(){
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedSalt =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt2Bytes));
PasswordHasher.Init(salt2Bytes, positiveAdler2);
Assert.Equal(encodedSalt,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler2,
currAdler.GetValue(hasher));
}
[Fact]
public void Route023457_3Bytes()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedSalt =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt3Bytes));
PasswordHasher.Init(salt3Bytes, positiveAdler2);
Assert.Equal(encodedSalt,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler2,
currAdler.GetValue(hasher));
}
[Fact]
public void Route023457_4Bytes()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedSalt =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt4Bytes));
PasswordHasher.Init(salt4Bytes, positiveAdler2);
Assert.Equal(encodedSalt,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler2,
currAdler.GetValue(hasher));
}
[Fact]
public void Route02467()
{
PasswordHasher.Init(saltASCII, positiveAdler);
PasswordHasher.Init(saltASCII2, negativeAdler);Assert.Equal(saltASCII2,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler, currAdler.GetValue(hasher));
}
[Fact]
public void Route023467_2Bytes()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedSalt =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt2Bytes));
PasswordHasher.Init(salt2Bytes, negativeAdler);
Assert.Equal(encodedSalt,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler, currAdler.GetValue(hasher));
}
[Fact]
public void Route023467_3Bytes()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedSalt =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt3Bytes));
PasswordHasher.Init(salt3Bytes, negativeAdler);
Assert.Equal(encodedSalt,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler, currAdler.GetValue(hasher));
}
[Fact]
public void Route023467_4Bytes()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedSalt =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt4Bytes));
PasswordHasher.Init(salt4Bytes, negativeAdler);
Assert.Equal(encodedSalt,
currSalt.GetValue(hasher).ToString());
Assert.Equal(positiveAdler, currAdler.GetValue(hasher));
}
}
public class TestGetHash{
PasswordHasher hasher = new PasswordHasher();
string saltASCII = "salt";
string saltASCII2 = "salt2";
uint positiveAdler = 1;
uint positiveAdler2 = 2;
string password = "password";
string passwordNotASCII = "𠃐汉Ęćźż字";
[Fact]
public void Route015()
{
Assert.Null(PasswordHasher.GetHash(null, saltASCII,
positiveAdler));
}
[Fact]
public void Route0245()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string first = PasswordHasher.GetHash(password);
string second = PasswordHasher.GetHash(password,
saltASCII2, positiveAdler2);
Assert.NotNull(second);
Assert.NotEqual(first, second);
}
[Fact]
public void Route02345()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string encodedPassword =
Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(passwordNotASCII)
);
string first = PasswordHasher.GetHash(passwordNotASCII);
string second = PasswordHasher.GetHash(passwordNotASCII,
saltASCII2, positiveAdler2);
string secondEncoded =
PasswordHasher.GetHash(encodedPassword, saltASCII2, positiveAdler2);
Assert.NotNull(second);
Assert.NotNull(secondEncoded);
Assert.NotEqual(first, second);Assert.Equal(second, secondEncoded);
}
[Fact]
public void TestIdempotencyASCII()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string first = PasswordHasher.GetHash(password);
PasswordHasher.Init(saltASCII, positiveAdler);
string second = PasswordHasher.GetHash(password);
Assert.Equal(first, second);
}
[Fact]
public void TestIdempotencyNotASCII()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string first = PasswordHasher.GetHash(passwordNotASCII);
PasswordHasher.Init(saltASCII, positiveAdler);
string second =
PasswordHasher.GetHash(passwordNotASCII);
Assert.Equal(first, second);
}
[Fact]
public void TestAdlerInfluence()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string first = PasswordHasher.GetHash(passwordNotASCII);
PasswordHasher.Init(saltASCII, positiveAdler2);
string second =
PasswordHasher.GetHash(passwordNotASCII);
Assert.NotEqual(first, second);
}
[Fact]
public void TestSaltInfluence()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string first = PasswordHasher.GetHash(passwordNotASCII);
PasswordHasher.Init(saltASCII2, positiveAdler);string second =
PasswordHasher.GetHash(passwordNotASCII);
Assert.NotEqual(first, second);
}
[Fact]
public void TestPasswordInfluence()
{
PasswordHasher.Init(saltASCII, positiveAdler);
string first = PasswordHasher.GetHash(passwordNotASCII);
PasswordHasher.Init(saltASCII, positiveAdler);
string second = PasswordHasher.GetHash(password);
Assert.NotEqual(first, second);
}
}
}
