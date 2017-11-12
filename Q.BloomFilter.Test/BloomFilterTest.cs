namespace Q.BloomFilter.Test
{
    using Xunit;

    public class BloomFilterTest
    {
        [Fact]
        public void lookup_should_return_false_when_bloomfilter_is_empty()
        {
            var bf = new BloomFilter<string>(20,10);

            Assert.False(bf.Lookup("empty"));
        }

        [Fact]
        public void lookup_should_return_true_when_bloomfilter_is_not_empty()
        {
            var bf = new BloomFilter<string>(20,10);
            bf.Add("Test String");
            Assert.True(bf.Lookup("Test String"));
        }

        [Fact]
        public void lookup_should_return_false_when_bloomfilter_is_not_empty()
        {
            var bf = new BloomFilter<string>(20, 10);
            bf.Add("Test String");
            Assert.False(bf.Lookup("Test"));
        }
    }
}
