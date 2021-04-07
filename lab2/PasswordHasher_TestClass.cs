using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using System;

namespace passwordHasher
{
    [TestClass]
    public class PasswordHasher_TestClass
    {
        [TestMethod]
        public void Constructor_PasswordHasher_Tests()
        {
            try {
                PasswordHasher p = new PasswordHasher();
                PasswordHasher p2 = new PasswordHasher();
                Assert.IsNotNull(p, "Password hasher instance p should not be null.");
                Assert.IsNotNull(p2, "Password hasher instance p2 should not be null.");
                Assert.AreNotEqual(p, p2, "Password hasher instances should not be are equal.");
            } catch (Exception e) {
                Assert.Fail(e.Message);
            } 
        }

        [TestMethod]
        public void Init_empty_string_and_null()
        {
            try {
                PasswordHasher.Init("", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void Init_empty_string_max_uint()
        {
            try {
                PasswordHasher.Init("", uint.MaxValue);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
            
        }

        [TestMethod]
        public void Init_null_parameters()
        {
            try {
                PasswordHasher.Init(null, 0);
                //PasswordHasher.Init("", null); //fails with error "cannot convert from '<null>' to 'uint'"
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
            
        }

        [TestMethod]
        public void Init_cyrillic()
        {
            try {
                PasswordHasher.Init("фівїґъыё", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Init_quotes()
        {
            try {
                //PasswordHasher.Init("""", 0); //fails with error "Syntax error, ',' expected"
                PasswordHasher.Init("''``", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            } 
        }

        [TestMethod]
        public void Init_escape_sequences()
        {
            try {
                PasswordHasher.Init("\n\r\b\\\v\a\t\f\0\'\"", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
            
        }

        [TestMethod]
        public void Init_long_string()
        {
            try {
                //PasswordHasher.Init(new string('*', 1000000000), 0); //runs very long
                //PasswordHasher.Init(new string('*', int.MaxValue), 0); //fails with error "Exception of type 'System.OutOfMemoryException' was thrown"
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Init_smiles()
        {
            try {
                PasswordHasher.Init(" 😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌  ", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Init_hieroglyphs()
        {
            try {
                PasswordHasher.Init("图路迪斯尼亚克", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Init_random_number()
        {
            try {
                PasswordHasher.Init("", (uint)new Random().Next());
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }  
        }

        [TestMethod]
        public void GetHash_empty_strings()
        {
            try {
                String hash = PasswordHasher.GetHash("", "", 5);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_modAlter_0()
        {
            try {
                String hash2 = PasswordHasher.GetHash("hello", "1", 0);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_modAlter_maxValue()
        {
            try {
                String hash3 = PasswordHasher.GetHash("hello", "0", uint.MaxValue);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_cyrillic()
        {
            try {
                String hash4 = PasswordHasher.GetHash("йцукенгшщзхфвапролджєячсмитьбю", "1", 5);
                String hash5 = PasswordHasher.GetHash("фівїґъыё", "0", 5);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_escape_sequence()
        {
            try {
                String hash6 = PasswordHasher.GetHash("\n\r\b\\\v\a\t\f\0\'\"", "0", 4);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_random_number()
        {
            try {
                String hash7 = PasswordHasher.GetHash("hello1", "0", (uint)new Random().Next());
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_nulls_parameters()
        {
            try {
                String hash8 = PasswordHasher.GetHash(null, null, 5);
                String hash9 = PasswordHasher.GetHash(null, null, null);
                String hash10 = PasswordHasher.GetHash(null);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_smiles()
        {
            try {
                String hash11 = PasswordHasher.GetHash("😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌", "😀 😃 😄 😁 😆 😅 😂 🤣 🥲 ☺️ 😊 😇 🙂 🙃 😉 😌");
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_ieroglyphes()
        {
            try {
                String hash12 = PasswordHasher.GetHash("图路迪斯尼亚克", "图路迪斯尼亚克"); 
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_long_strings()
        {
            try {
                //String hash13 = PasswordHasher.GetHash(new string('*', 1000000000), new string('*', 1000000000)); //runs very long
                //String hash13 = PasswordHasher.GetHash(new string('*', int.MaxValue), new string('*', int.MaxValue)); //fails with error "Exception of type 'System.OutOfMemoryException' was thrown."
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }   
        }

        [TestMethod]
        public void GetHash_same_parameters()
        {
            String hash = PasswordHasher.GetHash("hello", "", 5);
            String hash2 = PasswordHasher.GetHash("hello", "", 5);
            Assert.AreEqual(hash, hash2, "Hashes created with same parameters must be equal");
        }

        [TestMethod]
        public void GetHash_different_slats()
        {
            String hash3 = PasswordHasher.GetHash("hello", "0", 5);
            String hash4 = PasswordHasher.GetHash("hello", "1", 5);
            Assert.AreNotEqual(hash3, hash4, "Hashes created with same passwords, but different salts, must be different");
        }

        [TestMethod]
        public void GetHash_different_modAlter()
        {
            String hash5 = PasswordHasher.GetHash("hello", "0", 5);
            String hash6 = PasswordHasher.GetHash("hello", "0", 4);
            Assert.AreNotEqual(hash5, hash6, "Hashes created with same passwords, but different Mod Alters, must be different");

            String hash11 = PasswordHasher.GetHash("", "");
            Assert.IsNotNull(hash11, "Should not be null if doesn't have Mod Alter");
        }

        [TestMethod]
        public void GetHash_nulls_equal()
        {
            String hash9 = PasswordHasher.GetHash(null, null, 0);
            String hash10 = PasswordHasher.GetHash(null);
            Assert.AreEqual(hash9, hash10, "Hashes created with same null parameters should be equal");
        }

        [TestMethod]
        public void GetHash_nulls_null()
        {
            String hash9 = PasswordHasher.GetHash(null, null, 0);
            String hash10 = PasswordHasher.GetHash(null);
            Assert.IsNull(hash9, "Should be null if created by null parameters");
            Assert.IsNull(hash10, "Should be null if created by null parameters");
        }

        [TestMethod]
        public void GetHash_empty_parameters_notnull()
        {
            String hash12 = PasswordHasher.GetHash("");
            Assert.IsNotNull(hash12, "Should not be null if only one parameter is passed");
        }

        [TestMethod]
        public void GetHash_null_and_empty_parameters_equal()
        {
            String hash13 = PasswordHasher.GetHash("");
            String hash14 = PasswordHasher.GetHash("", null, null);
            Assert.AreEqual(hash13, hash14, "Hashes should be equal, when Salt and Mod Alter are initialized as zeros");
        }

        [TestMethod]
        public void GetHash_Init_Functions_Combined_Tests() 
        {
            try {
                String hash = PasswordHasher.GetHash("hello", null, null);
                PasswordHasher.Init(null, 0);
                String hash2 = PasswordHasher.GetHash("hello");
                Assert.AreEqual(hash, hash2, "Salt and Mod Alter should be initialized as zeros, hashes must be equal");

                String hash3 = PasswordHasher.GetHash("hello");
                PasswordHasher.Init("1", 0);
                String hash4 = PasswordHasher.GetHash("hello");
                Assert.AreNotEqual(hash3, hash4, "Change salt and Mod Alter by calling Init, hashes must be different");

                PasswordHasher.Init("123", 123);
                String hash5 = PasswordHasher.GetHash("hello");
                PasswordHasher.Init("1", 0);
                String hash6 = PasswordHasher.GetHash("hello");
                Assert.AreNotEqual(hash5, hash6, "Change salt and Mod Alter by calling Init before each, hashes must be different");

                PasswordHasher.Init("123", 123);
                String hash7 = PasswordHasher.GetHash("hello");
                String hash8 = PasswordHasher.GetHash("hello", "1", 0);
                Assert.AreNotEqual(hash7, hash8, "Change salt and Mod Alter by calling Init and passing arguments, hashes must be different");

                PasswordHasher.Init("123", 123);
                String hash9 = PasswordHasher.GetHash("hello", "123", 123);
                String hash10 = PasswordHasher.GetHash("hello");
                Assert.AreEqual(hash9, hash10, "Change salt and Mod Alter by calling Init and passing arguments, hashes must be equal");

                PasswordHasher.Init("123", 123);
                String hash11 = PasswordHasher.GetHash("hello");
                String hash12 = PasswordHasher.GetHash("hello");
                Assert.AreEqual(hash9, hash10, "Change salt and Mod Alter by calling Init and passing arguments, hashes must be equal");

            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
        }

    }
}
