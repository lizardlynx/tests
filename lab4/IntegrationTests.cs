using System;
using Xunit;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using IIG.CoSFE.DatabaseUtils;
using IIG.BinaryFlag;

namespace lab4
{
    [Collection("PasswordHasher_FileWorker")]
    public class PasswordHasher_FileWorker
    {
        [Theory]
        [InlineData("hello", "111", 3, "hash.txt")]
        [InlineData("", "0", 0, "hash2.txt")]
        [InlineData("йцукенгшщзхїфівапролджєячсмитьбю", "", 100000, "hash3.txt")]
        [InlineData("\n\r\b\\\v\a\t\f\0\'\"", "\n\r\b\\\v\a\t\f\0\'\"", 111111111, "hash4.txt")]
        [InlineData(" 😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌  ", "", 0, "hash5.txt")]
        [InlineData("图路迪斯尼亚克", "", 0, "hash6.txt")]
        public void GetHash_Write(String password, String salt, UInt32 mod, String fileName)
        {
            String hash = PasswordHasher.GetHash(password, salt, mod);
            var success = BaseFileWorker.TryWrite(hash, fileName);
            Assert.True(success);
            String x = BaseFileWorker.ReadAll(fileName);
            Assert.Equal(hash, x);
        }

        [Theory]
        [InlineData("hello", "111", 3, 6, "hash.txt")]
        [InlineData("", "0", 0, 5, "hash2.txt")]
        [InlineData("йцукенгшщзхїфівапролджєячсмитьбю", "", 100000, 1000000, "hash3.txt")]
        [InlineData("\n\r\b\\\v\a\t\f\0\'\"", "\n\r\b\\\v\a\t\f\0\'\"", 111111111, 5, "hash4.txt")]
        [InlineData(" 😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌  ", "", 5, 1, "hash5.txt")]
        [InlineData("图路迪斯尼亚克", "", 0, 5, "hash6.txt")]
        public void GetHash_TryWrite(String password, String salt, UInt32 mod, int timesToTry, String fileName)
        {
            String hash = PasswordHasher.GetHash(password, salt, mod);
            var success = BaseFileWorker.TryWrite(hash, fileName, timesToTry);
            Assert.True(success);
            String x = BaseFileWorker.ReadAll(fileName);
            Assert.Equal(hash, x);
        }

        [Theory]
        [InlineData("hello", "111", 3, 0, "1.txt")]
        [InlineData("", "0", 0, 0, "2.txt")]
        [InlineData("йцукенгшщзхїфівапролджєячсмитьбю", "", 100000, 0, "3.txt")]
        [InlineData("\n\r\b\\\v\a\t\f\0\'\"", "\n\r\b\\\v\a\t\f\0\'\"", 111111111, 0, "4.txt")]
        [InlineData(" 😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌  ", "", 5, 0, "5.txt")]
        [InlineData("图路迪斯尼亚克", "", 0, 0, "6.txt")]
        public void GetHash_TryWrite0Times(String password, String salt, UInt32 mod, int timesToTry, String fileName)
        {
            String hash = PasswordHasher.GetHash(password, salt, mod);
            var success = BaseFileWorker.TryWrite(hash, fileName, timesToTry);
            Assert.False(success);
        }

        [Fact]
        public void GetHash_Write_LongStrings()
        {
            String fileName = new string('*', 100000000)+".txt";
            String hash = PasswordHasher.GetHash(new string('*', 100000000), "salt", 4);
            var success = BaseFileWorker.Write(hash, fileName);
            Assert.True(success);
            String x = BaseFileWorker.ReadAll(fileName);
            Assert.Equal(hash, x);
        }

        [Theory]
        [InlineData("hello", "111", 3, "1.mp3")]
        [InlineData("", "0", 0, "2.txt")]
        [InlineData("йцукенгшщзхїфівапролджєячсмитьбю", "", 100000, "3")]
        [InlineData("\n\r\b\\\v\a\t\f\0\'\"", "\n\r\b\\\v\a\t\f\0\'\"", 111111111, "4.txt")]
        [InlineData(" 😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌  ", "", 5, "5.lol")]
        [InlineData("图路迪斯尼亚克", "", 0, "6.")]
        public void GetHash_ReadLines(String password, String salt, UInt32 mod, String fileName)
        {
            String hash = PasswordHasher.GetHash(password, salt, mod);
            var success = BaseFileWorker.Write(hash, fileName);
            Assert.True(success);
            String[] x = BaseFileWorker.ReadLines(fileName);
            Assert.Equal(hash, x[0]);
        }

        [Theory]
        [InlineData("hello", "111", 3, "999.mp3")]
        [InlineData("", "0", 0, "999.mp3")]
        [InlineData("йцукенгшщзхїфівапролджєячсмитьбю", "", 100000, "999.mp3")]
        [InlineData("\n\r\b\\\v\a\t\f\0\'\"", "\n\r\b\\\v\a\t\f\0\'\"", 111111111, "999.mp3")]
        [InlineData(" 😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌  ", "", 5, "999.mp3")]
        [InlineData("图路迪斯尼亚克", "", 0, "999.mp3")]
        public void GetHash_TryWrite_ReadLines(String password, String salt, UInt32 mod, String fileName)
        {
            String hash = PasswordHasher.GetHash(password, salt, mod);
            var success = BaseFileWorker.TryWrite(hash, fileName);
            Assert.True(success);
            String[] x = BaseFileWorker.ReadLines(fileName);
            Assert.Equal(hash, x[x.Length - 1]);
        }
    }
    [Collection("BinaryFlag_Database")]
    public class BinaryFlag_Database
    {
        private int index = 0;
        [Theory]
        [InlineData(3, true)]
        [InlineData(2, true)]
        [InlineData(32, true)]
        [InlineData(33, true)]
        [InlineData(64, true)]
        [InlineData(65, true)]
        public void AddFlag_GetFlag_True(ulong num, Boolean val)
        {
            index++;
            FlagpoleDatabaseUtils database = new FlagpoleDatabaseUtils(@"localhost", @"IIG.CoSWE.FlagPoleDB", false, @"SA", @"#Ananas208", 200);
            var binaryFlag = new MultipleBinaryFlag(num, val);
            var flagView = binaryFlag.ToString();
            var flag = binaryFlag.GetFlag();
            Boolean res = database.AddFlag(binaryFlag.ToString(), val);
            Assert.True(res);
            Boolean res2 = database.GetFlag(1, out flagView, out flag);
            Assert.True(res2);
        }
        
        [Theory]
        [InlineData(3, false)]
        [InlineData(2, false)]
        [InlineData(32, false)]
        [InlineData(33, false)]
        [InlineData(64, false)]
        [InlineData(65, false)]
        public void AddFlag_GetFlag_False(ulong num, Boolean val)
        {
            index++;
            FlagpoleDatabaseUtils database = new FlagpoleDatabaseUtils(@"localhost", @"IIG.CoSWE.FlagPoleDB", false, @"SA", @"#Ananas208", 200);
            var binaryFlag = new MultipleBinaryFlag(num, val);
            var flagView = binaryFlag.ToString();
            var flag = binaryFlag.GetFlag();
            Boolean res = database.AddFlag(binaryFlag.ToString(), val);
            Assert.True(res);
            Boolean res2 = database.GetFlag(1, out flagView, out flag);
            Assert.True(res2);
        }
    }
}
