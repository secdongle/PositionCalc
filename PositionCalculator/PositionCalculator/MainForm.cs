using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace PositionCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool radioButtonAddCheck = true;
        private bool radioButtonSubCheck = true;

        private void printMessage()
        {
            MessageBox.Show("输入的数据不完整，请重新输入或补充完整再操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void setAddResultControlVisibleStatus(bool flag)
        {
            labelAddFinalCost.Visible = flag;
            textBoxAddFinalCost.Visible = flag;
            labelAddFinalNum.Visible = flag;
            textBoxAddFinalNum.Visible = flag;
            labelAddMarketValue.Visible = flag;
            textBoxAddMarketValue.Visible = flag;
            labelAddProfitAndLossRatio.Visible = flag;
            textBoxAddProfitAndLossRatio.Visible = flag;
            labelAddProfitAndLossAmount.Visible = flag;
            textBoxAddProfitAndLossAmount.Visible = flag;
            labelAddPaybackIncrease.Visible = flag;
            textBoxAddPaybackIncrease.Visible = flag;
        }
        private void setSubResultControlVisibleStatus(bool flag)
        {
            labelSubFinalCost.Visible = flag;
            textBoxSubFinalCost.Visible = flag;
            labelSubFinalNum.Visible = flag;
            textBoxSubFinalNum.Visible = flag;
            labelSubMarketValue.Visible = flag;
            textBoxSubMarketValue.Visible = flag;
            labelSubProfitAndLossRatio.Visible = flag;
            textBoxSubProfitAndLossRatio.Visible = flag;
            labelSubProfitAndLossAmount.Visible = flag;
            textBoxSubProfitAndLossAmount.Visible = flag;
            labelSubPaybackIncrease.Visible = flag;
            textBoxSubPaybackIncrease.Visible = flag;
        }
        private void setAddFixResultControlVisibleStatus(bool flag)
        {
            labelAddFixAddPrice.Visible = flag;
            textBoxAddFixAddPrice.Visible = flag;
            labelAddFixAddNum.Visible = flag;
            textBoxAddFixAddNum.Visible = flag;
            labelAddFixFinalNum.Visible = flag;
            textBoxAddFixFinalNum.Visible = flag;
        }

        private void setSubFixResultControlVisibleStatus(bool flag)
        {
            labelSubFixSubPrice.Visible = flag;
            textBoxSubFixSubPrice.Visible = flag;
            labelSubFixSubNum.Visible = flag;
            textBoxSubFixSubNum.Visible = flag;
            labelSubFixFinalNum.Visible = flag;
            textBoxSubFixFinalNum.Visible = flag;
        }


        private void buttonAddCalc_Click(object sender, EventArgs e)
        {
            setAddResultControlVisibleStatus(false);

            if (textBoxAddInitPrice.Text != string.Empty & textBoxAddInitNum.Text!=string.Empty & textBoxAddPrice.Text!=string.Empty & textBoxAddNum.Text!=string.Empty)
            {
                setAddResultControlVisibleStatus(true);
                double initPrice = double.Parse(textBoxAddInitPrice.Text);
                double initNum = double.Parse(textBoxAddInitNum.Text);
                double addPrice = double.Parse(textBoxAddPrice.Text);
                double addNum = double.Parse(textBoxAddNum.Text);
                double newNum = initNum + addNum;
                double newMarketValue = newNum * addPrice;
                double newCost = (initPrice * initNum + addPrice * addNum) / newNum;
                double newProfitAndLossRatio = Math.Round((addPrice - newCost) / newCost, 4);
                double newProfitAndLossAmount = (addPrice - newCost) * newNum;
                textBoxAddFinalCost.Text = Math.Round(newCost, 3).ToString();
                textBoxAddFinalNum.Text = newNum.ToString();
                textBoxAddMarketValue.Text = newMarketValue.ToString();
                textBoxAddProfitAndLossRatio.Text = (newProfitAndLossRatio * 100).ToString() + "%";
                textBoxAddProfitAndLossAmount.Text = newProfitAndLossAmount.ToString();
                if (newProfitAndLossRatio < 0)
                {
                    double newPaybackIncrease = Math.Round((newCost - addPrice) / addPrice, 4);
                    textBoxAddPaybackIncrease.Text = (newPaybackIncrease * 100).ToString() + "%";
                }else
                {
                    textBoxAddPaybackIncrease.Visible = false;
                    labelAddPaybackIncrease.Visible = false;
                }
              
            }
            else
            {
                printMessage();
            }
           
        }


        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '.' && this.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void buttonAddReset_Click(object sender, EventArgs e)
        {
            textBoxAddInitPrice.Clear();
            textBoxAddInitNum.Clear();
            textBoxAddPrice.Clear();
            textBoxAddNum.Clear();
            textBoxAddFinalCost.Clear();
            textBoxAddFinalNum.Clear();
            textBoxAddMarketValue.Clear();
            textBoxAddProfitAndLossRatio.Clear();
            textBoxAddProfitAndLossAmount.Clear();
            textBoxAddPaybackIncrease.Clear();
            setAddResultControlVisibleStatus(false);
        }

        private void buttonSubCalc_Click(object sender, EventArgs e)
        {
            setSubResultControlVisibleStatus(false);
            if (textBoxSubInitPrice.Text != string.Empty & textBoxSubInitNum.Text != string.Empty & textBoxSubPrice.Text != string.Empty & textBoxSubNum.Text != string.Empty)
            {
                setSubResultControlVisibleStatus(true);
                double initPrice = double.Parse(textBoxSubInitPrice.Text);
                double initNum = double.Parse(textBoxSubInitNum.Text);
                double subPrice = double.Parse(textBoxSubPrice.Text);
                double subNum = double.Parse(textBoxSubNum.Text);
                double newNum = initNum - subNum;
                if (newNum <= 0.0) 
                {
                    setSubResultControlVisibleStatus(false);
                    MessageBox.Show("减仓数超过或等于持有数,请重新输入~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
                }else
                {
                    double newMarketValue = subPrice * newNum;
                    double newCost = (initPrice * initNum - subPrice * subNum) / newNum;
                    double newProfitAndLossRatio = Math.Round((subPrice - newCost) / newCost, 4);
                    double newProfitAndLossAmount = (subPrice - newCost) * newNum;

                    textBoxSubFinalCost.Text = Math.Round(newCost, 2).ToString();
                    textBoxSubFinalNum.Text = newNum.ToString();
                    textBoxSubMarketValue.Text = newMarketValue.ToString();
                    textBoxSubProfitAndLossRatio.Text = (newProfitAndLossRatio * 100).ToString() + "%";
                    textBoxSubProfitAndLossAmount.Text = newProfitAndLossAmount.ToString();

                    if (newProfitAndLossRatio < 0)
                    {
                        double newPaybackIncrease = Math.Round((newCost - subPrice) / subPrice, 4);
                        textBoxSubPaybackIncrease.Text = (newPaybackIncrease * 100).ToString() + "%";
                    }
                    else
                    {
                        textBoxSubPaybackIncrease.Visible = false;
                        labelSubPaybackIncrease.Visible = false;
                    }
                }
                               

            }
            else
            {
                printMessage();
            }

        }

        private void buttonSubReset_Click(object sender, EventArgs e)
        {
            textBoxSubInitPrice.Clear();
            textBoxSubInitNum.Clear();
            textBoxSubPrice.Clear();
            textBoxSubNum.Clear();
            textBoxSubFinalCost.Clear();
            textBoxSubFinalNum.Clear();
            textBoxSubMarketValue.Clear();
            textBoxSubProfitAndLossRatio.Clear();
            textBoxSubProfitAndLossAmount.Clear();
            textBoxSubPaybackIncrease.Clear();
            setSubResultControlVisibleStatus(false);
        }

        private void buttonAddFixCalc_Click(object sender, EventArgs e)
        {
            setAddFixResultControlVisibleStatus(false);
            if(textBoxAddFixInitPrice.Text != string.Empty & textBoxAddFixInitNum.Text != string.Empty & textBoxAddTargetCost.Text != string.Empty & textBoxAddFixPriceOrNum.Text != string.Empty)
            {
                setAddFixResultControlVisibleStatus(true);
                double initPrice = double.Parse(textBoxAddFixInitPrice.Text);
                double initNum = double.Parse(textBoxAddFixInitNum.Text);
                double targetCost = double.Parse(textBoxAddTargetCost.Text);
                double addPrice, addNum;
                if (radioButtonAddFixPriceOrNum.Checked == true)
                {
                    addPrice = double.Parse(textBoxAddFixPriceOrNum.Text);
                    addNum = (initPrice * initNum - targetCost * initNum) / (targetCost - addPrice);
                }
                else
                {
                    addNum = double.Parse(textBoxAddFixPriceOrNum.Text);
                    addPrice = (targetCost * (initNum + addNum) - initPrice * initNum) / addNum;
                }
                if (addNum < 0.0 | addPrice < 0.0)
                {
                    setAddFixResultControlVisibleStatus(false);
                    MessageBox.Show("无法达成目标~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }else
                {
                    textBoxAddFixAddPrice.Text = Math.Round(addPrice, 2).ToString();
                    textBoxAddFixAddNum.Text = Math.Round(addNum, 2).ToString();
                    textBoxAddFixFinalNum.Text = Math.Round((initNum + addNum), 2).ToString();
                }
               

            }else
            {
                printMessage();
            }
        }

        private void buttonAddFixReset_Click(object sender, EventArgs e)
        {
            textBoxAddFixInitPrice.Clear();
            textBoxAddFixInitNum.Clear();
            textBoxAddTargetCost.Clear();
            textBoxAddFixPriceOrNum.Clear();
            textBoxAddFixAddPrice.Clear();
            textBoxAddFixAddNum.Clear();
            textBoxAddFixFinalNum.Clear();
            setAddFixResultControlVisibleStatus(false);

        }

        private void radioButtonAddFixPriceOrNum_Click(object sender, EventArgs e)
        {
            if (radioButtonAddCheck)
            {
                radioButtonAddFixPriceOrNum.Checked = false;
                radioButtonAddCheck = false;
                
            }else
            {
                this.radioButtonAddFixPriceOrNum.Checked = true;
                radioButtonAddCheck = true;
            }
        }

        private void buttonSubFixCalc_Click(object sender, EventArgs e)
        {
            setSubFixResultControlVisibleStatus(false);
            if(textBoxSubFixInitPrice.Text != string.Empty & textBoxSubFixInitNum.Text != string.Empty & textBoxSubTargetCost.Text != string.Empty & textBoxSubFixPriceOrNum.Text != string.Empty)
            {
                setSubFixResultControlVisibleStatus(true);
                double initPrice = double.Parse(textBoxSubFixInitPrice.Text);
                double initNum = double.Parse(textBoxSubFixInitNum.Text);
                double targetCost = double.Parse(textBoxSubTargetCost.Text);
                double subPrice, subNum;
                if (radioButtonSubFixPriceOrNum.Checked == true)
                {
                    subPrice = double.Parse(textBoxSubFixPriceOrNum.Text);
                    subNum = (initPrice * initNum - targetCost * initNum) / (subPrice - targetCost);
                }
                else
                {
                    subNum = double.Parse(textBoxSubFixPriceOrNum.Text);
                    subPrice = (initPrice * initNum - targetCost * (initNum - subNum)) / subNum;
                }
                if (subPrice < 0.0 | subNum < 0.0)
                {
                    setSubFixResultControlVisibleStatus(false);
                    MessageBox.Show("无法达成目标~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else
                {
                    textBoxSubFixSubPrice.Text = Math.Round(subPrice, 2).ToString();
                    textBoxSubFixSubNum.Text = Math.Round(subNum, 2).ToString();
                    textBoxSubFixFinalNum.Text = Math.Round((initNum - subNum), 2).ToString();
                }
               
            }else
            {
                printMessage();
            }
        }

        private void buttonSubFixReset_Click(object sender, EventArgs e)
        {
            textBoxSubFixInitPrice.Clear();
            textBoxSubFixInitNum.Clear();
            textBoxSubTargetCost.Clear();
            textBoxSubFixPriceOrNum.Clear();
            textBoxSubFixSubPrice.Clear();
            textBoxSubFixSubNum.Clear();
            textBoxSubFixFinalNum.Clear();
            setSubFixResultControlVisibleStatus(false);

        }

        private void radioButtonSubFixPriceOrNum_Click(object sender, EventArgs e)
        {
            if (radioButtonSubCheck)
            {
                radioButtonSubFixPriceOrNum.Checked = false;
                radioButtonSubCheck = false;

            }
            else
            {
                this.radioButtonSubFixPriceOrNum.Checked = true;
                radioButtonSubCheck = true;
            }

        }

        private void buttonComeBackCalc_Click(object sender, EventArgs e)
        {
            if (textBoxLossRatio.Text != string.Empty)
            {
                double lossRatio = double.Parse(textBoxLossRatio.Text);
                double result = Math.Round((1 / (1 - lossRatio/100) - 1), 4);
                textBoxComeBackPaybackIncrease.Text = (result * 100).ToString();

            }else
            {
                printMessage();
            }
        }

        private void buttonComeBackCalcTwo_Click(object sender, EventArgs e)
        {
            if (textBoxComeBackProfitRatio.Text != string.Empty)
            {
                double profitRatio = double.Parse(textBoxComeBackProfitRatio.Text);
                double result = Math.Round(1 - (1 / (1 + profitRatio/100)), 4);
                textBoxComeBackPaybackDecline.Text = (result * 100).ToString();

            }
            else
            {
                printMessage();
            }
        }

    }
}
