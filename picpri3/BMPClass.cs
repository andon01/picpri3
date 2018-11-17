using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//https://www.united-bears.co.jp/blog/archives/909 「Android で Bitmap を安全に操作する」
//https://techbooster.org/android/application/14228/ 「Matrixクラスを使ってBitmapを加工する」
//http://ichitcltk.hustle.ne.jp/gudon2/index.php?pageType=file&id=Android039_Graphics6_Bitmap 「グラフィックス（６）-Bitmapの描画とMatrixの操作」

namespace picpri3
{
    class BMPClass
    {
        public Bitmap BMP ;
        public Matrix MTX = new Matrix();

        public BMPClass(){}
    }
}