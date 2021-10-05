using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElasticityCalculator {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void btnCalcPriceElasticity_Click(object sender, EventArgs e) {
            int rcE;
            int rcCE;
            int rcIED;
            double rcOriginalRevenue = 0;
            double rcNewRevenue = 0;
            double revenueChange;

            try {
                CalculatePriceElasticity(out rcE, out rcCE, out rcIED, out rcOriginalRevenue, out rcNewRevenue);
            } catch (DivideByZeroException) {
                rcE = -2;
                rcCE = -2;
                rcIED = -2;
            } catch (Exception) {
                rcE = -1;
                rcCE = -3;
                rcIED = -3;
            }

            if (rcE > 0) ToggleInputLock(true);

            if (rcE == 0) {
                lblPEOut.Text = "Elasticity: Failed, not enough information";
                lblExpenditure.Text = "Expenditure: N/A";
            } else if (rcE == 1) {
                lblPEOut.Text = "Elasticity: Elastic";
                lblExpenditure.Text = "Expenditure: Increased";
            } else if (rcE == 2) {
                lblPEOut.Text = "Elasticity: Unit Elastic";
                lblExpenditure.Text = "Expenditure: Unchanged";
            } else if (rcE == 3) {
                lblPEOut.Text = "Elasticity: Inelastic";
                lblExpenditure.Text = "Expenditure: Unknown";
            } else if (rcE == 4) {
                lblPEOut.Text = "Elasticity: Perfectly Inelastic";
                lblExpenditure.Text = "Expenditure: N/A";
            } else if (rcE == 5) {
                lblPEOut.Text = "Elasticity: Perfectly Elastic";
                lblExpenditure.Text = "Expenditure: N/A";
            } else if (rcE == -2) {
                lblPEOut.Text = "Elasticity: Divide by Zero Error (might be perfectly elastic)";
                lblExpenditure.Text = "Expenditure: N/A";
            } else {
                lblPEOut.Text = "Elasticity: Failed, unknown error";
                lblExpenditure.Text = "Expenditure: Failed, unknown error";
            }

            if (rcCE == 1) lblCrossElasticity.Text = "Cross Elasticity: Substitute";
            else if (rcCE == 2) lblCrossElasticity.Text = "Cross Elasticity: Complement";
            else if (rcCE == 0) lblCrossElasticity.Text = "Cross Elasticity: No connection";
            //else if (rcCE == -1) lblCrossElasticity.Text = "Cross Elasticity: Failed, E unknown";
            else lblCrossElasticity.Text = "Cross Elasticity: Failed, see Elasticity error";

            if (rcIED == 1) lblIncomeElasticity.Text = "Income Elasticity: Income elastic, normal good";
            else if (rcIED == 2) lblIncomeElasticity.Text = "Income Elasticity: Income inelastic, normal good";
            else if (rcIED == 3) lblIncomeElasticity.Text = "Income Elasticity: Negative relationship, inferior good";
            else lblIncomeElasticity.Text = "Income Elastictiy: Failed, see Elasticity error";

            if (!double.IsNegativeInfinity(rcOriginalRevenue)) {
                revenueChange = rcNewRevenue - rcOriginalRevenue;
                if (revenueChange > 0) lblRevenueChange.Text =  $"Revenue Change: {revenueChange} (increased)";
                else if (revenueChange < 0) lblRevenueChange.Text =  $"Revenue Change: {revenueChange} (decreased)";
                else lblRevenueChange.Text = $"Revenue Change: {revenueChange} (no change)";

                lblOriginalRevenue.Text = $"Original Revenue: {rcOriginalRevenue}";
                lblNewRevenue.Text = $"New Revenue: {rcNewRevenue}";
            } else {
                lblOriginalRevenue.Text = "Original Revenue: Unknown, not enough information";
                lblNewRevenue.Text = "New Revenue: Unknown, not enough information";
                lblRevenueChange.Text = "Revenue Change: Unknown, not enough information";
            }

        }

        private void CalculatePriceElasticity(out int elasticityType, out int crossElasticityType, out int incomeElasticityType, out double originalRevenue, out double newRevenue) {

            double Q1;
            double Q2;
            double P1;
            double P2;
            double dQ;
            double dP;
            double E;

            bool knownQ1 = double.TryParse(txtPEQ1.Text, out Q1);
            bool knownQ2 = double.TryParse(txtPEQ2.Text, out Q2);
            bool knownP1 = double.TryParse(txtPEP1.Text, out P1);
            bool knownP2 = double.TryParse(txtPEP2.Text, out P2);
            bool knownDQ = double.TryParse(txtPEdQ.Text, out dQ);
            bool knownDP = double.TryParse(txtPEdP.Text, out dP);
            bool knownE = double.TryParse(txtPEE.Text, out E);

            bool somethingSolved = true;
            bool allSolved = false;

            while (somethingSolved && !allSolved) {
                somethingSolved = false;

                if (!knownDQ && (knownQ1 && knownQ2)) { //solve dQ
                    dQ = ((Q2 - Q1) / ((Q2 + Q1)/2)) * 100;
                    knownDQ = true;
                    somethingSolved = true;
                    txtPEdQ.Text = dQ.ToString();
                }

                if (!knownDP && (knownP1 && knownP2)) { //solve dP
                    dP = ((P2 - P1) / ((P2 + P1)/2)) * 100;
                    knownDP = true;
                    somethingSolved = true;
                    txtPEdP.Text = dP.ToString();
                }

                if (!knownE && (knownDQ && knownDP)) { //solve E
                    E = dQ / dP;
                    knownE = true;
                    somethingSolved = true;
                    txtPEE.Text = E.ToString();
                }

                if (knownE) {
                    if ((knownQ1 ? 1 : 0) + (knownQ2 ? 1 : 0) + (knownP1 ? 1 : 0) + (knownP2 ? 1 : 0) == 3) {
                        if (!knownQ1) { //solve Q1
                            Q1 = (Q2 * P2 + Q2 * P1 - E * Q2 * P2 + E * Q2 * P1) / (E * P2 - E * P1 + P2 + P1);
                            knownQ1 = true;
                            somethingSolved = true;
                            txtPEQ1.Text = Q1.ToString();
                        } else if (!knownQ2) { //solve Q2
                            Q2 = -(-Q1 * P2 - Q1 * P1 - E * Q1 * P2 + E * Q1 * P1) / (P2 + P1 - E * P2 + E * P1);
                            knownQ2 = true;
                            somethingSolved = true;
                            txtPEQ2.Text = Q2.ToString();
                        } else if (!knownP1) { //solve P1
                            P1 = -(Q2 * P2 - Q1 * P2 - E * Q2 * P2 - E * Q1 * P2) / (Q2 - Q1 + E * Q2 + E * Q1);
                            knownP1 = true;
                            somethingSolved = true;
                            txtPEP1.Text = P1.ToString();
                        } else if (!knownP2) { //solve P2
                            P2 = (Q2 * P1 - Q1 * P1 + E * Q2 * P1 + E * Q1 * P1) / (E * Q2 + E * Q1 - Q2 + Q1);
                            knownP2 = true;
                            somethingSolved = true;
                            txtPEP2.Text = P2.ToString();
                        }
                    }

                    if (!knownDP && knownDQ) { //solve dP
                        dP = dQ / E;
                        knownDP = true;
                        somethingSolved = true;
                        txtPEdP.Text = dP.ToString();
                    } else if (!knownDQ && knownDP) { //solve dQ
                        dQ = dP * E;
                        knownDQ = true;
                        somethingSolved = true;
                        txtPEdQ.Text = dQ.ToString();
                    }
                }

                allSolved = knownQ1 && knownQ2 && knownP1 && knownP2 && knownDQ && knownDP && knownE;
            }

            bool usefulSolved = knownDQ && knownDP && knownE;

            if (usefulSolved) {
                if (double.IsInfinity(E)) elasticityType = 5;
                else if (Math.Abs(E) > 1) elasticityType = 1;
                else if (Math.Abs(E) == 1) elasticityType = 2;
                else if (E == 0) elasticityType = 4;
                else if (Math.Abs(E) < 1) elasticityType = 3;
                else elasticityType = 0;
            } else {
                elasticityType = 0;
            }

            if (knownE) {
                if (E > 0) crossElasticityType = 1;
                else if (E < 0) crossElasticityType = 2;
                else crossElasticityType = 0;

                if (E > 1) incomeElasticityType = 1;
                else if (E > 0) incomeElasticityType = 2;
                else incomeElasticityType = 3;
            } else {
                crossElasticityType = -1;
                incomeElasticityType = -1;
            }

            if (knownQ1 && knownQ2 && knownP1 && knownP2) {
                originalRevenue = Q1 * P1;
                newRevenue = Q2 * P2;
            } else {
                originalRevenue = double.NegativeInfinity;
                newRevenue = double.NegativeInfinity;
            }
        }

        private void btnClearPriceElasticity_Click(object sender, EventArgs e) {
            txtPEQ1.Clear();
            txtPEQ2.Clear();
            txtPEP1.Clear();
            txtPEP2.Clear();
            txtPEdQ.Clear();
            txtPEdP.Clear();
            txtPEE.Clear();

            lblPEOut.Text = "Elasticity: <waiting>";
            lblExpenditure.Text = "Expenditure: <waiting>";
            lblCrossElasticity.Text = "Cross Elasticity: <waiting>";
            lblIncomeElasticity.Text = "Income Elasticity: <waiting>";
            lblOriginalRevenue.Text = "Original Revenue: <waiting>";
            lblNewRevenue.Text = "New Revenue: <waiting>";
            lblRevenueChange.Text = "Revenue Change: <waiting>";

            ToggleInputLock(false);
        }

        private void ToggleInputLock(bool lck) {
            txtPEQ1.ReadOnly = lck;
            txtPEQ2.ReadOnly = lck;
            txtPEP1.ReadOnly = lck;
            txtPEP2.ReadOnly = lck;
            txtPEdQ.ReadOnly = lck;
            txtPEdP.ReadOnly = lck;
            txtPEE.ReadOnly = lck;
            btnCalcPriceElasticity.Enabled = !lck;
        }
    }
}
