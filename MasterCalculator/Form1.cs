using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterCalculator {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e) {
            string result = "";
            bool constsOk = true;
            double a = Decimal.ToDouble(nudA.Value);
            double b = Decimal.ToDouble(nudB.Value);
            double c = Decimal.ToDouble(nudC.Value);
            double d = Decimal.ToDouble(nudD.Value);
            double degree = Decimal.ToDouble(nudDeg.Value);
            double logba;

            if (nudA.Value >= 1) {
                result += "const a: OK\r\n";
            } else {
                result += "const a: FAIL (a < 1)\r\n";
                constsOk = false;
            }

            if (nudB.Value > 1) {
                result += "const b: OK\r\n";
            } else {
                result += "const b: FAIL (b <= 1)\r\n";
                constsOk = false;
            }

            if (nudC.Value >= 1) {
                result += "const c: OK\r\n";
            } else {
                result += "cont c: FAIL (c < 1)\r\n";
                constsOk = false;
            }

            if (nudD.Value >= 0) {
                result += "const d: OK\r\n";
            } else {
                result += "const d: FAIL (d < 0)\r\n";
            }

            if (!constsOk) {
                result += "\r\nThe master theorem does not apply.";
            } else {
                logba = Math.Log(a, b);

                if (degree < logba) {
                    result += "\r\nCase 1 of the master theorem applies.\r\n";
                    result += $"a = {a}\r\n";
                    result += $"b = {b}\r\n";
                    result += $"c = {c}\r\n";
                    result += $"d = {d}\r\n";
                    result += $"ε < {Math.Floor((logba - degree) * 10.0) / 10.0}\r\n";
                    result += $"Therefore T(n) ∈ Θ(n^log{b}({a}))";
                } else if (degree == logba) {
                    result += "\r\nCase 2 of the master theorem applies.\r\n";
                    result += $"a = {a}\r\n";
                    result += $"b = {b}\r\n";
                    result += $"c = {c}\r\n";
                    result += $"d = {d}\r\n";
                    result += $"Therefore T(n) ∈ Θ(n^log{b}({a}) * logn)";
                } else if (degree > logba) {
                    result += "\r\nCase 3 of the master theorem MAY APPLY.\r\n";
                    result += $"a = {a}\r\n";
                    result += $"b = {b}\r\n";
                    result += $"c = {c}\r\n";
                    result += $"d = {d}\r\n";
                    result += $"ε < {Math.Round((degree - logba) * 10.0) / 10.0}\r\n";
                    result += $"You must find a κ < 1 and an n0 such that for any n > n0, af(n/{b}) <= κf(n) using a proof.\r\n";
                    result += $"Therefore T(n) ∈ Θ(f(n))";
                } else {
                    result += "The master theorem does not apply.";
                }
            }

            txtResult.Text = result;
            
        }
    }
}
