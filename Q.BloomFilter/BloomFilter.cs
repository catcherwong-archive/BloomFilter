namespace Q.BloomFilter
{
    using System;
    using System.Collections;

    public class BloomFilter<T>
    {
        /// <summary>
        /// Size of the bloom filter in bits (m)
        /// </summary>
        private readonly int _bitSize;
        /// <summary>
        /// Size of the set (n)
        /// </summary>
        private readonly int _setSize;
        /// <summary>
        /// Number of hashing functions (k)
        /// </summary>
        private readonly int _numberOfHashes;
        /// <summary>
        /// Bloom filter array
        /// </summary>
        private readonly BitArray _bitArray;

        /// <summary>
        /// Initializes the bloom filter and sets the optimal number of hashes. 
        /// </summary>
        /// <param name="bitSize">Size of the bloom filter in bits (m)</param>
        /// <param name="setSize">Size of the set (n)</param>
        public BloomFilter(int bitSize, int setSize)
        {
            _bitSize = bitSize;
            _setSize = setSize;
            _bitArray = new BitArray(bitSize);
            _numberOfHashes = GetOptimalNumberOfHashes(_bitSize, _setSize);
        }

        /// <summary>
        /// Adds an item to the bloom filter.
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Add(T item)
        {
            Random _random = new Random(Hash(item));

            for (int i = 0; i < _numberOfHashes; i++)
                _bitArray.Set(_random.Next(_bitSize) % _bitSize, true);
        }

        /// <summary>
        /// Checks whether an item is probably in the set. 
        /// False positives  are possible, but false negatives are not.
        /// </summary>
        /// <param name="item">Item to be checked</param>
        /// <returns>True if the set probably contains the item</returns>
        public bool Lookup(T item)
        {
            Random _random = new Random(Hash(item));

            for (int i = 0; i < _numberOfHashes; i++)
            {
                if (!_bitArray[_random.Next(_bitSize) % _bitSize])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Hashing function for an object
        /// </summary>
        /// <param name="item">Any object</param>
        /// <returns>Hash of that object</returns>
        private int Hash(T item)
        {
            return item.GetHashCode();
        }

        /// <summary>
        /// Calculates the optimal number of hashes based on 
        /// bloom filter bit size and set size.
        /// </summary>
        /// <param name="bitSize">Size of the bloom filter in bits (m)</param>
        /// <param name="setSize">Size of the set (n)</param>
        /// <returns>The optimal number of hashes</returns>
        private int GetOptimalNumberOfHashes(int bitSize, int setSize)
        {
            return (int)Math.Ceiling((bitSize / setSize) * Math.Log(2.0));
        }
    }
}
