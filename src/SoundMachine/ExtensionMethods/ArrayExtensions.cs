﻿using System;
using System.Linq;

namespace SoundMachine.ExtensionMethods
{
    public static class ArrayExtensions
    {
        public static float[] ToNormalizedFloat(this short[] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var result = new float[data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                result[i] = (float) data[i] / short.MaxValue;
            }

            return result;
        }
    }
}