using System;
using System.Collections.Generic;
using System.Text;

namespace RC4
{
    public class RC4Algorithm
    {
        const int N = 256;
        int[] _sbox;
        readonly byte[] _seedKey;
        byte[] _data;
        int _i, _j;

        public RC4Algorithm(byte[] seedKey, byte[] data)
        {
            _seedKey = seedKey;
            _data = data;
        }

        public RC4Algorithm(byte[] seedKey)
        {
            _seedKey = seedKey;
        }

        public byte[] Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public byte[] EnDeCrypt()
        {
            Rc4Initialize();

            var cipher = new List<byte>();

            foreach (var t in _data)
            {
                var k = GetNextKeyByte();

                var cipherBy = (t) ^ k;

                cipher.Add(Convert.ToByte(cipherBy));
            }

            return cipher.ToArray();
        }

        public byte GetNextKeyByte()
        {
            _i = (_i + 1) % N;
            _j = (_j + _sbox[_i]) % N;

            var tempSwap = _sbox[_i];

            _sbox[_i] = _sbox[_j];
            _sbox[_j] = tempSwap;

            var k = _sbox[(_sbox[_i] + _sbox[_j]) % N];

            return (byte)k;
        }

        public void Rc4Initialize()
        {
            Initialize();
        }

        public void Rc4Initialize(int drop)
        {
            Initialize();

            for (var i = 0; i < drop; i++)
            {
                GetNextKeyByte();
            }
        }

        private void Initialize()
        {
            _i = 0;
            _j = 0;

            _sbox = new int[N];
            var key = new int[N];

            for (var a = 0; a < N; a++)
            {
                key[a] = _seedKey[a % _seedKey.Length];
                _sbox[a] = a;
            }

            var b = 0;
            for (var a = 0; a < N; a++)
            {
                b = (b + _sbox[a] + key[a]) % N;
                var tempSwap = _sbox[a];

                _sbox[a] = _sbox[b];
                _sbox[b] = tempSwap;
            }
        }
    }
}