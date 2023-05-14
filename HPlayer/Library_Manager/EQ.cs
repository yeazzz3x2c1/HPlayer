using System;
using System.Windows;
using Un4seen.Bass;
namespace HPlayer
{
    class EQ
    {
        private static int[] fxEQ = { 0 };
        private static int iCount;
        public static BASS_DX8_REVERB ReverBb = new BASS_DX8_REVERB();
        public static int strm = 0;
        public static int fx = 0;
        public void New()
        {
            int[] fxEQ = new int[16];
        }

        public static void SetEQ_10(int handle)
        {
            BASS_DX8_PARAMEQ paramEQ = new BASS_DX8_PARAMEQ();
            try
            {
                iCount = 0;
                while (iCount < 16)
                {
                    fxEQ[iCount] = Bass.BASS_ChannelSetFX(handle, BASSFXType.BASS_FX_DX8_PARAMEQ, 0);
                    iCount += 1;
                }
                paramEQ.fBandwidth = 18.0F;
                paramEQ.fGain = 0.0F;
                paramEQ.fCenter = 55.0F;
                Bass.BASS_FXSetParameters(fxEQ[0], paramEQ);
                paramEQ.fCenter = 77.0F;
                Bass.BASS_FXSetParameters(fxEQ[1], paramEQ);
                paramEQ.fCenter = 110.0F;
                Bass.BASS_FXSetParameters(fxEQ[2], paramEQ);
                paramEQ.fCenter = 156.0F;
                Bass.BASS_FXSetParameters(fxEQ[3], paramEQ);
                paramEQ.fCenter = 220.0F;
                Bass.BASS_FXSetParameters(fxEQ[4], paramEQ);
                paramEQ.fCenter = 311.0F;
                Bass.BASS_FXSetParameters(fxEQ[5], paramEQ);
                paramEQ.fCenter = 440.0F;
                Bass.BASS_FXSetParameters(fxEQ[6], paramEQ);
                paramEQ.fCenter = 622.0F;
                Bass.BASS_FXSetParameters(fxEQ[7], paramEQ);
                paramEQ.fCenter = 880.0F;
                Bass.BASS_FXSetParameters(fxEQ[8], paramEQ);
                paramEQ.fCenter = 1000.0F;
                Bass.BASS_FXSetParameters(fxEQ[9], paramEQ);
                paramEQ.fCenter = 2000.0F;
                Bass.BASS_FXSetParameters(fxEQ[10], paramEQ);
                paramEQ.fCenter = 4000.0F;
                Bass.BASS_FXSetParameters(fxEQ[11], paramEQ);
                paramEQ.fCenter = 5000.0F;
                Bass.BASS_FXSetParameters(fxEQ[12], paramEQ);
                paramEQ.fCenter = 7000.0F;
                Bass.BASS_FXSetParameters(fxEQ[13], paramEQ);
                paramEQ.fCenter = 10000.0F;
                Bass.BASS_FXSetParameters(fxEQ[14], paramEQ);
                paramEQ.fCenter = 14000.0F;
                Bass.BASS_FXSetParameters(fxEQ[15], paramEQ);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public static void UpdateEQ(int band, float gain)
        {
            BASS_DX8_PARAMEQ paramEQ = new BASS_DX8_PARAMEQ();
            try
            {
                if (Bass.BASS_FXGetParameters(fxEQ[band], paramEQ))
                {
                    paramEQ.fGain = gain;
                    Bass.BASS_FXSetParameters(fxEQ[band], paramEQ);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public static void DisEQ(int handle)
        {
            iCount = 0;
            while (iCount < 16)
            {
                Bass.BASS_ChannelRemoveFX(handle, fxEQ[iCount]);
                iCount += 1;
            }
        }
    }
}
