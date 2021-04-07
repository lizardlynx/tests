using System;
using Xunit;
using IIG.BinaryFlag;

namespace lab3
{
    [Collection ("Constructor tests")]
    public class ConstructorTests {
        [Theory]
        [InlineData(2, true, "route 2-3-6-9-11-15")]
        [InlineData(3, true, "route 2-3-6-9-11-15")]
        [InlineData(32, true, "route 2-3-6-9-11-15")]
        [InlineData(33, true, "route 2-4-7-10-11-15")]
        [InlineData(64, true, "route 2-4-7-10-11-15")]
        [InlineData(65, true, "route 2-5-8-14-15")]
        [InlineData(17179868704, true, "route 2-5-8-14-15")]
        public void LengthInitialValueTrueValidParametersPassed(ulong length, bool initialValue, string route)
        {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, initialValue);
            } catch (Exception e) {
                Assert.True(false,route);
            }
        }
        
        [Theory]
        [InlineData(2, "route 2-3-6-9-11-12-15")]
        [InlineData(3, "route 2-3-6-9-11-12-15")]
        [InlineData(32, "route 2-3-6-9-11-12-15")]
        [InlineData(33, "route 2-4-7-10-11-12-15")]
        [InlineData(64, "route 2-4-7-10-11-12-15")]
        [InlineData(65, "route 2-5-8-14-15")]
        [InlineData(17179868704, "route 2-5-8-14-15")]
        public void LengthOnlyValidParametersPassed(ulong length, string route)
        {
            try {
                var binaryFlag = new MultipleBinaryFlag(length);
            } catch (Exception e) {
                Assert.True(false,route);
            }
        }

        [Theory]
        [InlineData(2, false, "route 2-3-6-9-11-12-15")]
        [InlineData(3, false, "route 2-3-6-9-11-12-15")]
        [InlineData(32, false, "route 2-3-6-9-11-12-15")]
        [InlineData(33, false, "route 2-4-7-10-11-12-15")]
        [InlineData(64, false, "route 2-4-7-10-11-12-15")]
        [InlineData(65, false, "route 2-5-8-14-15")]
        [InlineData(17179868704, false, "route 2-5-8-14-15")]
        public void LengthInitialValueFalseValidParametersPassed(ulong length, bool initialValue, string route)
        {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, initialValue);
            } catch (Exception e) {
                Assert.True(false,route);
            }
        }

        [Theory]
        [InlineData(1, "route 2-13-15")]
        [InlineData(0, "route 2-13-15")]
        [InlineData(17179868705, "route 2-13-15")]
        [InlineData(17179868706, "route 2-13-15")]
        public void WrongLengthPassed(ulong length, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length);
                Assert.True(false, route);
            } catch (Exception e) {
                Assert.True(true);
            }
        }
    }

    [Collection ("Checking set flag")]
    public class SetFlagTests {
        [Theory]
        [InlineData(2, false, "should be different")]
        public void InstancesAreNotEqual(ulong length, bool initialValue, string route)
        {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, initialValue);
                var binaryFlag2 = new MultipleBinaryFlag(length, initialValue);
                Assert.Equal(binaryFlag, binaryFlag2);
                Assert.True(false, route);
            } catch (Exception e) {
                Assert.True(true);
            }
        }
        
        [Theory]
        [InlineData(2, true, "initial value should be true")]
        [InlineData(2, false, "initial value should be false")]
        [InlineData(34, true, "initial value should be true")]
        [InlineData(34, false, "initial value should be false")]
        [InlineData(100, true, "initial value should be true")]
        [InlineData(100, false, "initial value should be false")]
        public void CheckInitialValueInput(ulong length, bool boolean, string message) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, boolean);
            } catch (Exception e) {
                Assert.True(false, message);
            }
        }

        [Theory]
        [InlineData(2, "initial value should be true")]
        [InlineData(34, "initial value should be true")]
        [InlineData(100, "initial value should be true")]
        public void CheckInitialValueEmptyParameters(ulong length, string message) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length);
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, true);
            } catch(Exception e) {
                Assert.True(false, message);
            }
        }

        [Theory]
        [InlineData(2, false, 0, "should be false")]
        [InlineData(2, false, 1, "should be false")]
        [InlineData(2, true, 0, "should be true")]
        [InlineData(2, true, 1, "should be true")]
        public void CheckSetFlagOnce(ulong length, bool initialValue, ulong position, string message) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, initialValue);
                binaryFlag.SetFlag(position);
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, initialValue);
            } catch (Exception e) {
                Assert.True(false, message);
            }
        }

        [Theory]
        [InlineData(2, false, new ulong[] {0, 1}, true,"route 1-2-4-5")]
        [InlineData(2, false, new ulong[] {0}, false, "route 1-2-4-5")]
        [InlineData(2, false, new ulong[] {}, false, "route 1-2-4-5")]
        [InlineData(34, false, new ulong[] {0, 5, 15, 31}, false, "route 1-2-4-5")]
        [InlineData(34, false, new ulong[] {}, false, "route 1-2-4-5")]
        [InlineData(1000, false, new ulong[] {0, 100, 200, 300}, false, "route 1-2-4-5")]
        [InlineData(1000, false, new ulong[] {}, false, "route 1-2-4-5")]
        [InlineData(2, true, new ulong[] {0, 1}, true,"route 1-2-4-5")]
        [InlineData(2, true, new ulong[] {0}, true, "route 1-2-4-5")]
        [InlineData(2, true, new ulong[] {}, true, "route 1-2-4-5")]
        [InlineData(34, true, new ulong[] {0, 5, 15, 31}, true, "route 1-2-4-5")]
        [InlineData(34, true, new ulong[] {}, true, "route 1-2-4-5")]
        [InlineData(1000, true, new ulong[] {0, 100, 200, 300}, true, "route 1-2-4-5")]
        [InlineData(1000, true, new ulong[] {}, true, "route 1-2-4-5")]
        public void CheckSetFlagManyValidParameters_false(ulong length, bool initialValue, ulong[] positions, bool expected, string message) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, initialValue);
                foreach (ulong position in positions) {
                    binaryFlag.SetFlag(position);
                }
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, expected);
            } catch (Exception e) {
                Assert.True(false, message);
            }
        }

        [Theory]
        [InlineData(2, false, true,"route 1-2-4-5")]
        [InlineData(34, false, true, "route 1-2-4-5")]
        [InlineData(1000, false, true, "route 1-2-4-5")]
        [InlineData(2, true, true,"route 1-2-4-5")]
        [InlineData(34, true, true, "route 1-2-4-5")]
        [InlineData(1000, true, true, "route 1-2-4-5")]
        public void CheckSetFlagManyValidParameters_true(ulong length, bool initialValue, bool expected, string message) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, initialValue);
                for (ulong i = 0; i < length; i++) {
                    binaryFlag.SetFlag(i);
                }
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, expected);
            } catch (Exception e) {
                Assert.True(false, message);
            }
        }

        [Theory]
        [InlineData(2, false, 3, "route 1-2-3-5")]
        [InlineData(34, false, 35, "route 1-2-3-5")]
        [InlineData(1000, false, 1001, "route 1-2-3-5")]
        [InlineData(2, true, 3, "route 1-2-3-5")]
        [InlineData(34, true, 35, "route 1-2-3-5")]
        [InlineData(1000, true, 1001, "route 1-2-3-5")]
        public void ThrowArgumentOutOfRangeExceptionOnWrongPosition(ulong length, bool boolean, ulong position, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.SetFlag(position);
                Assert.True(false,route);
            } catch (Exception e) {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData(2, false, 3, "route 1-5")]
        [InlineData(34, false, 35, "route 1-5")]
        [InlineData(1000, false, 1001, "route 1-5")]
        [InlineData(2, true, 3, "route 1-5")]
        [InlineData(34, true, 35, "route 1-5")]
        [InlineData(1000, true, 1001, "route 1-5")]
        public void ThrowArgumentOutOfRangeExceptionOnWrongPositionSetFlag(ulong length, bool boolean, ulong position, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.Dispose();
                binaryFlag.SetFlag(position);
                Assert.True(true);
            } catch (Exception e) {
                Assert.True(false, route);
            }
        }

    }

    [Collection("Check dispose")]
    public class CheckDisposeTests {
         [Theory]
        [InlineData(2, false, "route 1-2-3-4-5")]
        [InlineData(34, false, "route 1-2-3-4-5")]
        [InlineData(1000, false, "route 1-2-3-4-5")]
        [InlineData(2, true,"route 1-2-3-4-5")]
        [InlineData(34, true, "route 1-2-3-4-5")]
        [InlineData(1000, true, "route 1-2-3-4-5")]
        public void CheckDispose(ulong length, bool boolean, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.Dispose();
            } catch (Exception e){
                Assert.True(false, route);
            }
        }

        [Theory]
        [InlineData(2, false, "route 1-5")]
        [InlineData(34, false, "route 1-5")]
        [InlineData(1000, false, "route 1-5")]
        [InlineData(2, true,"route 1-5")]
        [InlineData(34, true, "route 1-5")]
        [InlineData(1000, true, "route 1-5")]
        public void CheckDisposeTwice(ulong length, bool boolean, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.Dispose();
                binaryFlag.Dispose();
            } catch (Exception e){
                Assert.True(false, route);
            }
        }
    }

    [Collection("Get Flag tests")]
    public class GetFlagTests {
        [Theory]
        [InlineData(2, "route 1-3")]
        [InlineData(34, "route 1-3")]
        [InlineData(1000, "route 1-3")]
        public void CheckGetFlagIfNotExists(ulong length, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length);
                binaryFlag.Dispose();
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, null);
            } catch (Exception e) {
                Assert.True(false, route);
            }
            
        }

        [Theory]
        [InlineData(2, false, "route 1-2-3")]
        [InlineData(34, false, "route 1-2-3")]
        [InlineData(1000, false, "route 1-2-3")]
        [InlineData(2, true, "route 1-2-3")]
        [InlineData(34, true, "route 1-2-3")]
        [InlineData(1000, true, "route 1-2-3")]
        public void CheckGetFlagIfExists(ulong length, bool boolean, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, boolean);
            } catch (Exception e) {
                Assert.True(false, route);
            }
            
        }
    }

    [Collection("Reset Flag tests")]
    public class ResetFlagTests {
        [Theory]
        [InlineData(2, false, "route 1-5")]
        [InlineData(34, false, "route 1-5")]
        [InlineData(1000, false, "route 1-5")]
        [InlineData(2, true, "route 1-5")]
        [InlineData(34, true, "route 1-5")]
        [InlineData(1000, true, "route 1-5")]
        public void ResetIfNotExists(ulong length, bool boolean, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.Dispose();
                binaryFlag.ResetFlag(0);
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, null);
            } catch (Exception e) {
                Assert.True(false, route);
            }
        }

        [Theory]
        [InlineData(2, false, "route 1-2-3-5")]
        [InlineData(34, false, "route 1-2-3-5")]
        [InlineData(1000, false, "route 1-2-3-5")]
        [InlineData(2, true, "route 1-2-3-5")]
        [InlineData(34, true, "route 1-2-3-5")]
        [InlineData(1000, true, "route 1-2-3-5")]
        public void ResetIfPositionBiggerThenLength(ulong length, bool boolean, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.ResetFlag(length);
                Assert.True(false,route);
            } catch (Exception e) {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData(2, false, "route 1-2-4-5")]
        [InlineData(34, false, "route 1-2-4-5")]
        [InlineData(1000, false, "route 1-2-4-5")]
        [InlineData(2, true, "route 1-2-4-5")]
        [InlineData(34, true, "route 1-2-4-5")]
        [InlineData(1000, true, "route 1-2-4-5")]
        public void ResetValid(ulong length, bool boolean, string route) {
            try {
                var binaryFlag = new MultipleBinaryFlag(length, boolean);
                binaryFlag.ResetFlag(0);
                var flag = binaryFlag.GetFlag();
                Assert.Equal(flag, false);
            } catch (Exception e) {
                Assert.True(false, route);
            }
        }
    }
}
