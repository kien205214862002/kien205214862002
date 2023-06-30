using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class TaiKhoan
    {
        private string tk;
        private string mk;
        public TaiKhoan()
        {

        }
        public TaiKhoan(string tk, string mk)
        {
            this.tk = tk;
            this.mk = mk;

        }

        public string Tk { get => tk; set => tk = value; }
        public string Mk { get => mk; set => mk = value; }
    }
}
