using System;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Print;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

// https://akira-watson.com/ 「Android アプリを作る」
//https://akira-watson.com/android/canvas-paint.html 「[Android] Canvas Paint で円や矩形を描画する」

namespace picpri3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private static int REQUEST_GALLERY = 0;
        ImageView image1;
        bool setBmp = false;
        Bitmap bmp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            //変数とリソースの紐付け
            image1 = (Android.Widget.ImageView)FindViewById(Resource.Id.imageView1);
            image1.Click += Image1_Click;
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            /*if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else*/ if (id == Resource.Id.nav_gallery)
            {
                // ギャラリー呼び
                Intent intent = new Intent();
                intent.SetType("image/*");
                intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(intent, REQUEST_GALLERY);
            }
            else if (id == Resource.Id.nav_printer)
            {
                DoPhotoPrint();
            }
            /*else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }*/
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (REQUEST_GALLERY == requestCode && Result.Ok == resultCode)
            {
                //呼び出し元の確認
                //Toast.MakeText(this, string.Format("resultCode:{0} data:{1}", requestCode, data), ToastLength.Short).Show();
                //InputStream in = GetContentResolver().openInputStream(data.getData());
                //Bitmap img = BitmapFactory.DecodeStream(in);
                //Stream in = GetContentResolver().openInputStream(data.getData());
                bmp = BitmapFactory.DecodeStream(ContentResolver.OpenInputStream(data.Data));
//                Bitmap bmp2;
                if (!setBmp)
                {
                    setBmp = true;
                }
                image1.SetImageBitmap(bmp);
            }
        }
        private void Image1_Click(object sender, EventArgs e)
        {
            if (setBmp)
            {
                // 画像を切り替える
                Matrix MTX = new Matrix();
                MTX.PostRotate(45);
                //Bitmap bmp2;
                bmp = Bitmap.CreateBitmap(bmp, 0, 0, bmp.Width, bmp.Height, MTX, true);
                image1.SetImageBitmap(bmp);
                image1.Invalidate();
            }
        }
        public void DoPhotoPrint()
        {
            // プリンターヘルパーを生成し、設定を行う
            PrintHelper printHelper = new PrintHelper(this);
            printHelper.ScaleMode = PrintHelper.ScaleModeFit;

            // 印刷情報を設定
            if (!setBmp)
            {
                return;
            }
            String jobName = "test print";

            // 印刷
            printHelper.PrintBitmap(jobName, bmp);
        }


    }
}

