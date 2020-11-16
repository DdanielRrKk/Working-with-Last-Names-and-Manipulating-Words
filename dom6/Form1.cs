using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.CompilerServices;

namespace dom6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // dom6
        Label combLbl = new Label();
        ComboBox lName = new ComboBox();

        Label egnLbl = new Label();
        TextBox egn = new TextBox();
        
        Label countLbl = new Label();
        Label count = new Label();

        ListView li = new ListView();

        ImageList imgL = new ImageList();

        // border
        Label borderLabel = new Label();

        //dom7
        TextBox bigBox = new TextBox();

        Button wordsBtn = new Button();
        Button splitBtn = new Button();
        Button sortBtn = new Button();
        Button uniqueBtn = new Button();

        ListBox lBox = new ListBox();

        private void Form1_Load(object sender, EventArgs e)
        {
            //==========================За Ресетване

            //==========================За Ресетване

            imgL.Images.Add(Image.FromFile(@"C:\Users\Daniel\Pictures\Dimension.ico"));
            imgL.Images.Add(Image.FromFile(@"C:\Users\Daniel\Pictures\MiningModel.ico"));
            imgL.Images.Add(Image.FromFile(@"C:\Users\Daniel\Pictures\Role.ico"));
            imgL.Images.Add(Image.FromFile(@"C:\Users\Daniel\Pictures\ssms.ico"));
            imgL.Images.Add(Image.FromFile(@"C:\Users\Daniel\Pictures\XSLTFile.ico"));
            imgL.Images.Add(Image.FromFile(@"C:\Users\Daniel\Pictures\sync.ico"));

            this.Text = "Даниел Костов 18621439 КСТ 4А група";
            this.Icon = new Icon(@"C:\Users\Daniel\Pictures\Cube.ico");
            this.BackgroundImage = new Bitmap(@"C:\Users\Daniel\Pictures\Amazing-Ireland-Nature-Wallpaper-Background-Image.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            this.Size = new System.Drawing.Size(1500, 600);
            this.Top = 0;
            this.Left= 0;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.setMenu();
            this.setList();

            this.setEGNSearcher();
            this.setNameSearcher();
            this.setCounter();


            borderLabel.Location = new System.Drawing.Point(750, 0);
            borderLabel.Size = new System.Drawing.Size(5, 800);
            borderLabel.BackColor = Color.Black;
            this.Controls.Add(borderLabel);


            this.makeDom7();
        }

        //==================MENU
        private void setMenu()
        {
            MenuStrip menu = new MenuStrip();


            ToolStripMenuItem mNew = new ToolStripMenuItem("&Добави ИМЕ");
            mNew.Click += new System.EventHandler(mNew_Click);

            ToolStripMenuItem mi1 = new ToolStripMenuItem("&Добави ИМЕ 2");
            mi1.Click += new System.EventHandler(mNew2_Click);

            mNew.Image = imgL.Images[0];
            mi1.Image = imgL.Images[0];


            ToolStripMenuItem mFind = new ToolStripMenuItem("&Намери по ЕГН");
            mFind.Click += new System.EventHandler(mFind_Click);

            ToolStripMenuItem mi2 = new ToolStripMenuItem("&Намери по ЕГН 2");
            mi2.Click += new System.EventHandler(mFind2_Click);

            mFind.Image = imgL.Images[1];
            mi2.Image = imgL.Images[1];


            ToolStripMenuItem mUpdate = new ToolStripMenuItem("&Актуализирай");
            mUpdate.Click += new System.EventHandler(mUpdate_Click);

            ToolStripMenuItem mi3 = new ToolStripMenuItem("&Актуализирай 2");
            mi3.Click += new System.EventHandler(mUpdate2_Click);

            mUpdate.Image = imgL.Images[2];
            mi3.Image = imgL.Images[2];


            ToolStripMenuItem mRemove = new ToolStripMenuItem("&Отстрани по ЕГН");
            mRemove.Click += new System.EventHandler(mRemove_Click);

            ToolStripMenuItem mi4 = new ToolStripMenuItem("&Отстрани по ЕГН 2");
            mi4.Click += new System.EventHandler(mRemove2_Click);

            mRemove.Image = imgL.Images[3];
            mi4.Image = imgL.Images[3];


            ToolStripMenuItem mEnd = new ToolStripMenuItem("&Край");
            mEnd.Click += new System.EventHandler(mEnd_Click);

            mEnd.Image = imgL.Images[4];


            ToolStripMenuItem mBonus = new ToolStripMenuItem("&Бонус");

            mBonus.Image = imgL.Images[5];

            mBonus.DropDownItems.Add(mi1);
            mBonus.DropDownItems.Add(mi2);
            mBonus.DropDownItems.Add(mi3);
            mBonus.DropDownItems.Add(mi4);

            mBonus.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem mi5 = new ToolStripMenuItem("&Sort Unique");
            mi5.Click += new System.EventHandler(sortUnique);
            mBonus.DropDownItems.Add(mi5);

            ToolStripMenuItem mi6 = new ToolStripMenuItem("&Words List");
            mi6.Click += new System.EventHandler(wordsList);
            mBonus.DropDownItems.Add(mi6);

            menu.Items.Add(mNew);
            menu.Items.Add(mFind);
            menu.Items.Add(mUpdate);
            menu.Items.Add(mRemove);
            menu.Items.Add(mEnd);
            menu.Items.Add(mBonus);

            menu.BackColor = Color.White;

            this.Controls.Add(menu);
        }

        //===============SEARCH BY NAME
        private void setNameSearcher()
        {
            combLbl.Text = "ФАМИЛИЯ";
            combLbl.Font = new Font("Times New Roman", 15);
            combLbl.Location = new System.Drawing.Point(150, 120);
            combLbl.Size = new System.Drawing.Size(200, 20);

            lName.Location = new System.Drawing.Point(450, 120);
            lName.Size = new System.Drawing.Size(100, 20);
            lName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(name_KeyPress);

            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            for(int i=0; i < li.Items.Count; i++)
            {
                source.Add(li.Items[i].SubItems[1].Text);
            }
            lName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lName.AutoCompleteCustomSource = source;
            lName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.Controls.Add(combLbl);
            this.Controls.Add(lName);
        }

        //===============SEARCH BY EGN
        private void setEGNSearcher()
        {
            egnLbl.Text = "ЕГН";
            egnLbl.Font = new Font("Times New Roman", 15);
            egnLbl.Location = new System.Drawing.Point(150, 170);
            egnLbl.Size = new System.Drawing.Size(200, 20);
    
            egn.Location = new System.Drawing.Point(450, 170);
            egn.Size = new System.Drawing.Size(100, 20);
            egn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(egn_KeyPress);

            this.Controls.Add(egnLbl);
            this.Controls.Add(egn);
        }

        //=================COUNTER
        private void setCounter()
        {
            countLbl.Text = "Брой на елементите в колекцията: ";
            countLbl.Font = new Font("Times New Roman", 15);
            countLbl.Location = new System.Drawing.Point(150, 220);
            countLbl.Size = new System.Drawing.Size(200, 20);

            count.Text = li.Items.Count.ToString();
            count.Font = new Font("Times New Roman", 15);
            count.Location = new System.Drawing.Point(450, 220);
            count.Size = new System.Drawing.Size(50, 20);

            this.Controls.Add(countLbl);
            this.Controls.Add(count);
        }

        //================LIST
        private void setList()
        {
            li.Name = "Li";
            li.View = View.Details;
            li.Columns.Add("ЕГН", 290);
            li.Columns.Add("ФАМИЛИЯ", 290);


            li.Location = new System.Drawing.Point(100, 250);
            li.Size = new System.Drawing.Size(600, 300);
            li.BackColor = Color.LightBlue;
            li.ForeColor = Color.Black;

            listWithData();

            this.Controls.Add(li);
        }

        //==========ADD DATA TO LIST
        private void listWithData()
        {
            int counter = 0;

            string[] lines = System.IO.File.ReadAllLines(@"D:\stDom\names.txt");

            for (int i = 1; i < lines.Length; i = i + 2) 
            {
                counter++;
                string str1;
                string str2;

                if (counter <= 9) { str1 = counter.ToString() + "00"; }
                else if (counter <= 99) { str1 = counter.ToString() + "0"; }
                else { str1 = counter.ToString(); }

                if (i <= 9) { str2 = i.ToString() + "0"; }
                else { str2 = i.ToString(); }

                string[] tempStr = { lines[i - 1], lines[i] };
                ListViewItem itm = new ListViewItem(tempStr);
                li.Items.Add(itm);
            }

        }

        //===========================MENU FUNCTIONS
        private void mNew2_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            Label lName = new Label();
            TextBox tName = new TextBox();
            Label lEGN = new Label();
            TextBox tEGN = new TextBox();
            Button bOK = new Button();
            Button bCANCEL = new Button();

            form.Text = "Добави ИМЕ";
            lName.Text = "Въведи Фамилия";
            lEGN.Text = "Въведи ЕГН";

            bOK.Text = "OK";
            bCANCEL.Text = "Cancel";
            bOK.DialogResult = DialogResult.OK;
            bCANCEL.DialogResult = DialogResult.Cancel;

            lName.SetBounds(10, 20, 10, 20);
            tName.SetBounds(10, 40, 380, 20);
            lEGN.SetBounds(10, 70, 10, 20);
            tEGN.SetBounds(10, 90, 380, 20);
            bOK.SetBounds(220, 170, 75, 20);
            bCANCEL.SetBounds(300, 170, 75, 20);

            lName.AutoSize = true;
            lEGN.AutoSize = true;

            tName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(name_KeyPress);
            tEGN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(egn_KeyPress);

            form.ClientSize = new Size(400, 200);
            form.Controls.AddRange(new Control[] { lName, tName, lEGN, tEGN, bOK, bCANCEL });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = bOK;
            form.CancelButton = bCANCEL;

            DialogResult res = form.ShowDialog();

            if (res == DialogResult.OK)
            {
                string valName = tName.Text;
                string valEgn = tEGN.Text;

                if (valName == "")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведете Фамилия !", "Внимание!", buttons);
                }
                else if (valEgn == "")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
                }
                else if (valEgn.Length < 10)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
                }
                else if (findEGN(valEgn) == true)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Дублиране на ЕГН !", "Внимание!", buttons);
                }
                else
                {
                    string[] tempStr = { valEgn, valName };
                    ListViewItem itm = new ListViewItem(tempStr);
                    li.Items.Add(itm);

                    this.resetFile();
                    this.setNameSearcher();
                    this.setCounter();
                }
            }
        }

        private void mFind2_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            Label lEGN = new Label();
            TextBox tEGN = new TextBox();
            Button bOK = new Button();
            Button bCANCEL = new Button();

            form.Text = "Намери";
            lName.Text = "Въведи Фамилия";
            lEGN.Text = "Въведи ЕГН";

            bOK.Text = "OK";
            bCANCEL.Text = "Cancel";
            bOK.DialogResult = DialogResult.OK;
            bCANCEL.DialogResult = DialogResult.Cancel;

            lEGN.SetBounds(10, 20, 10, 20);
            tEGN.SetBounds(10, 40, 380, 20);
            bOK.SetBounds(220, 70, 75, 20);
            bCANCEL.SetBounds(300, 70, 75, 20);

            lName.AutoSize = true;
            lEGN.AutoSize = true;

            tEGN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(egn_KeyPress);

            form.ClientSize = new Size(400, 100);
            form.Controls.AddRange(new Control[] { lEGN, tEGN, bOK, bCANCEL });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = bOK;
            form.CancelButton = bCANCEL;

            DialogResult res = form.ShowDialog();

            if (res == DialogResult.OK)
            {
                string valEgn = tEGN.Text;

                if (valEgn == "")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
                }
                else if (valEgn.Length < 10)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
                }
                else if (findEGN(valEgn) == false)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Няма Фамилия с такова ЕГН !", "Внимание!", buttons);
                }
                else
                {
                    for (int i = 0; i < li.Items.Count; i++)
                    {
                        if (valEgn == li.Items[i].SubItems[0].Text)
                        {
                            li.Items[i].Selected = true;
                            li.EnsureVisible(i);
                        }
                    }
                }
            }
        }

        private void mUpdate2_Click(object sender, EventArgs e)
        {

            Form form = new Form();
            Label lName = new Label();
            TextBox tName = new TextBox();
            Label lEGN = new Label();
            TextBox tEGN = new TextBox();
            Button bOK = new Button();
            Button bCANCEL = new Button();

            form.Text = "Актуализирай";
            lName.Text = "Въведи Фамилия";
            lEGN.Text = "Въведи ЕГН";

            bOK.Text = "OK";
            bCANCEL.Text = "Cancel";
            bOK.DialogResult = DialogResult.OK;
            bCANCEL.DialogResult = DialogResult.Cancel;

            lName.SetBounds(10, 20, 10, 20);
            tName.SetBounds(10, 40, 380, 20);
            lEGN.SetBounds(10, 70, 10, 20);
            tEGN.SetBounds(10, 90, 380, 20);
            bOK.SetBounds(220, 170, 75, 20);
            bCANCEL.SetBounds(300, 170, 75, 20);

            lName.AutoSize = true;
            lEGN.AutoSize = true;

            tName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(name_KeyPress);
            tEGN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(egn_KeyPress);

            form.ClientSize = new Size(400, 200);
            form.Controls.AddRange(new Control[] { lName, tName, lEGN, tEGN, bOK, bCANCEL });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = bOK;
            form.CancelButton = bCANCEL;

            DialogResult res = form.ShowDialog();

            if (res == DialogResult.OK)
            {
                string eVal = tEGN.Text;
                string nVal = tName.Text;

                if (nVal == "")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведете Фамилия !", "Внимание!", buttons);
                }
                else if (eVal == "")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
                }
                else if (eVal.Length < 10)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
                }
                else if (findEGN(eVal) == false)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Няма Фамилия с такова ЕГН !", "Внимание!", buttons);
                }
                else
                {
                    for (int i = 0; i < li.Items.Count; i++)
                    {
                        if (eVal == li.Items[i].SubItems[0].Text)
                        {
                            li.Items[i].SubItems[1].Text = nVal;
                            li.EnsureVisible(i);

                            this.resetFile();
                            this.setNameSearcher();
                        }
                    }
                }
            }
        }

        private void mRemove2_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            Label lEGN = new Label();
            TextBox tEGN = new TextBox();
            Button bOK = new Button();
            Button bCANCEL = new Button();

            form.Text = "Изтрий";
            lName.Text = "Въведи Фамилия";
            lEGN.Text = "Въведи ЕГН";

            bOK.Text = "OK";
            bCANCEL.Text = "Cancel";
            bOK.DialogResult = DialogResult.OK;
            bCANCEL.DialogResult = DialogResult.Cancel;

            lEGN.SetBounds(10, 20, 10, 20);
            tEGN.SetBounds(10, 40, 380, 20);
            bOK.SetBounds(220, 70, 75, 20);
            bCANCEL.SetBounds(300, 70, 75, 20);

            lName.AutoSize = true;
            lEGN.AutoSize = true;

            tEGN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(egn_KeyPress);

            form.ClientSize = new Size(400, 100);
            form.Controls.AddRange(new Control[] { lEGN, tEGN, bOK, bCANCEL });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = bOK;
            form.CancelButton = bCANCEL;

            DialogResult res = form.ShowDialog();

            if (res == DialogResult.OK)
            {
                string valEgn = tEGN.Text;

                if (valEgn == "")
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
                }
                else if (valEgn.Length < 10)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
                }
                else if (findEGN(valEgn) == false)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Няма Фамилия с такова ЕГН !", "Внимание!", buttons);
                }
                else
                {
                    for (int i = 0; i < li.Items.Count; i++)
                    {
                        if (valEgn == li.Items[i].SubItems[0].Text)
                        {
                            li.Items[i].Remove();

                            this.resetFile();
                            this.setNameSearcher();
                            this.setCounter();
                        }
                    }
                }
            }
        }

        //==================MENU 1
        private void mNew_Click(object sender, EventArgs e)
        {
            string valName = lName.Text;
            string valEgn = egn.Text;

            if (valName == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведете Фамилия !", "Внимание!", buttons);
            }
            else if (valEgn == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
            }
            else if (valEgn.Length < 10)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
            }
            else if (findEGN(valEgn) == true)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Дублиране на ЕГН !", "Внимание!", buttons);
            }
            else
            {
                string[] tempStr = { valEgn, valName };
                ListViewItem itm = new ListViewItem(tempStr);
                li.Items.Add(itm);

                this.resetFile();
                this.setNameSearcher();
                this.setCounter();
            }
        }

        private void mFind_Click(object sender, EventArgs e)
        {

            string val = egn.Text;

            if (val == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
            }
            else if (val.Length < 10)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
            }
            else if (findEGN(val) == false)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Няма Фамилия с такова ЕГН !", "Внимание!", buttons);
            }
            else
            {
                for (int i = 0; i < li.Items.Count; i++)
                {
                    if (val == li.Items[i].SubItems[0].Text)
                    {
                        li.Items[i].Selected = true;
                        li.EnsureVisible(i);
                    }
                }
            }
        }

        private void mUpdate_Click(object sender, EventArgs e)
        {
            string eVal = egn.Text;
            string nVal = lName.Text;

            if (nVal == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведете Фамилия !", "Внимание!", buttons);
            }
            else if (eVal == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
            }
            else if (eVal.Length < 10)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
            }
            else if (findEGN(eVal) == false)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Няма Фамилия с такова ЕГН !", "Внимание!", buttons);
            }
            else
            {
                for (int i = 0; i < li.Items.Count; i++)
                {
                    if (eVal == li.Items[i].SubItems[0].Text)
                    {
                        li.Items[i].SubItems[1].Text = nVal;
                        li.EnsureVisible(i);

                        this.resetFile();
                        this.setNameSearcher();
                    }
                }
            }
        }

        private void mRemove_Click(object sender, EventArgs e)
        {
            string val = egn.Text;

            if (val == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведете ЕГН !", "Внимание!", buttons);
            }
            else if (val.Length < 10)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Въведеното ЕГН е непълно !", "Внимание!", buttons);
            }
            else if (findEGN(val) == false)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Няма Фамилия с такова ЕГН !", "Внимание!", buttons);
            }
            else
            {
                for (int i = 0; i < li.Items.Count; i++)
                {
                    if (val == li.Items[i].SubItems[0].Text)
                    {
                        li.Items[i].Remove();

                        this.resetFile();
                        this.setNameSearcher();
                        this.setCounter();
                    }
                }
            }
        }

        private void mEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c))
            {
                if (c != (Char)Keys.Back)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Фамилията не може да има числа !", "Внимание!", buttons);
                    e.Handled = true;
                }
            }
            if (c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || char.IsControl(c))
            {

            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Фамилията трябва да е на Български !", "Внимание!", buttons);
                e.Handled = true;
            }

            TextBox tb = new TextBox();
            ComboBox cb = new ComboBox();

            try
            {
                tb = (TextBox)sender;
            }
            catch (Exception ex)
            {
                cb = (ComboBox)sender;
            }

            if (tb.Text.Length >= 15 || cb.Text.Length >= 15)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Фамилията е до 15 букви !", "Внимание!", buttons);
                e.Handled = true;
            }
        }

        private void egn_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c) == false)
            {
                if (c != (Char)Keys.Back)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("ЕГН не може да има букви !", "Внимание!", buttons);
                    e.Handled = true;
                }
            }

            TextBox tb = (TextBox)sender;
            if (tb.Text.Length >= 10)
            {
                if (c != (Char)Keys.Back)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("ЕГН е до 10 числа !", "Внимание!", buttons);
                    e.Handled = true;
                }
            }
        }

        private void resetFile()
        {
            System.IO.File.WriteAllText(@"D:\stDom\names.txt", string.Empty);

            using (StreamWriter sw = File.AppendText(@"D:\stDom\names.txt"))
            {
                for (int i = 0; i < li.Items.Count; i++)
                {
                    sw.WriteLine(li.Items[i].SubItems[0].Text);
                    sw.WriteLine(li.Items[i].SubItems[1].Text);
                }
            }
        }

        private bool findEGN(string s)
        {
            bool flag = false;

            for (int i = 0; i < li.Items.Count; i++)
            {
                if (s == li.Items[i].SubItems[0].Text)
                {
                    flag = true;
                }
            }

            return flag;
        }

        //==========================================================================================

        private void makeDom7()
        {
            bigBox.Location = new System.Drawing.Point(780, 50);
            bigBox.Height = 400;
            bigBox.Width = 440;
            bigBox.Multiline = true;

            wordsBtn.Location = new System.Drawing.Point(780, 460);
            wordsBtn.Height = 40;
            wordsBtn.Width = 200;
            wordsBtn.Text = "Words";

            splitBtn.Location = new System.Drawing.Point(1020, 460);
            splitBtn.Height = 40;
            splitBtn.Width = 200;
            splitBtn.Text = "Split";

            sortBtn.Location = new System.Drawing.Point(780, 510);
            sortBtn.Height = 40;
            sortBtn.Width = 200;
            sortBtn.Text = "Sort";

            uniqueBtn.Location = new System.Drawing.Point(1020, 510);
            uniqueBtn.Height = 40;
            uniqueBtn.Width = 200;
            uniqueBtn.Text = "Unique";

            lBox.Location = new System.Drawing.Point(1250, 50);
            lBox.Height = 500;
            lBox.Width = 200;

            wordsBtn.Click += new System.EventHandler(words);
            splitBtn.Click += new System.EventHandler(split);
            sortBtn.Click += new System.EventHandler(sort);
            uniqueBtn.Click += new System.EventHandler(unique);

            this.Controls.Add(bigBox);
            this.Controls.Add(wordsBtn);
            this.Controls.Add(splitBtn);
            this.Controls.Add(sortBtn);
            this.Controls.Add(uniqueBtn);
            this.Controls.Add(lBox);
        }

        private void words(Object sender, EventArgs e)
        {
            bigBox.Text = bigBox.Text.Replace("!", " ");
            bigBox.Text = bigBox.Text.Replace("?", " ");
            bigBox.Text = bigBox.Text.Replace(":", " ");
            bigBox.Text = bigBox.Text.Replace(";", " ");
            bigBox.Text = bigBox.Text.Replace(".", " ");
            bigBox.Text = bigBox.Text.Replace(",", " ");
            bigBox.Text = bigBox.Text.Replace(")", " ");
            bigBox.Text = bigBox.Text.Replace("(", " ");
        }

        private void split(Object sender, EventArgs e)
        {
            lBox.Items.Clear();

            List<string> lStr = new List<string>();

            string[] rowStr = bigBox.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < rowStr.Length; i++)
            {
                string tempS = rowStr[i].Trim();
                string[] splitStr = tempS.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < splitStr.Length; j++)
                {
                    lStr.Add(splitStr[j].Trim());
                }
            }

            for (int i = 0; i < lStr.Count; i++)
            {
                lBox.Items.Add(lStr[i].Trim());
            }
        }

        private void sort(Object sender, EventArgs e)
        {
            lBox.Items.Clear();

            List<string> lStr = new List<string>();

            string[] rowStr = bigBox.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            for(int i = 0; i < rowStr.Length; i++)
            {
                string tempS = rowStr[i].Trim();
                string[] splitStr = tempS.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < splitStr.Length; j++)
                {
                    lStr.Add(splitStr[j].Trim());
                }
            }

            lStr = lStr.OrderBy(q => q).ToList();

            for (int i = 0; i < lStr.Count; i++)
            {
                lBox.Items.Add(lStr[i].Trim());
            }
        }

        private void unique(Object sender, EventArgs e)
        {
            lBox.Items.Clear();

            List<string> lStr = new List<string>();
            List<string> tempL = new List<string>();

            string[] rowStr = bigBox.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < rowStr.Length; i++)
            {
                string tempS = rowStr[i].Trim();
                string[] splitStr = tempS.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < splitStr.Length; j++)
                {
                    lStr.Add(splitStr[j].Trim());
                }
            }

            for (int i = 0; i < lStr.Count; i++)
            {
                if (tempL.Contains(lStr[i].Trim()) == false) 
                {
                    tempL.Add(lStr[i].Trim());
                    lBox.Items.Add(lStr[i].Trim());
                }
            }
        }

        private void wordsList(Object sender, EventArgs e)
        {

            for (int i = 0; i < lBox.Items.Count; i++)
            {
                try
                {
                    if (lBox.Items[i].ToString().Trim() == "!" || lBox.Items[i].ToString().Trim() == "?")
                    {
                        lBox.Items.RemoveAt(i);
                    }
                    if (lBox.Items[i].ToString().Trim() == ":" || lBox.Items[i].ToString().Trim() == ";")
                    {
                        lBox.Items.RemoveAt(i);
                    }
                    if (lBox.Items[i].ToString().Trim() == "," || lBox.Items[i].ToString().Trim() == ".")
                    {
                        lBox.Items.RemoveAt(i);
                    }
                    if (lBox.Items[i].ToString().Trim() == "(" || lBox.Items[i].ToString().Trim() == ")")
                    {
                        lBox.Items.RemoveAt(i);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void sortUnique(Object sender, EventArgs e)
        {
            lBox.Items.Clear();

            List<string> lStr = new List<string>();
            List<string> tempL = new List<string>();

            string[] rowStr = bigBox.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < rowStr.Length; i++)
            {
                string tempS = rowStr[i].Trim();
                string[] splitStr = tempS.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < splitStr.Length; j++)
                {
                    lStr.Add(splitStr[j].Trim());
                }
            }

            lStr = lStr.OrderBy(q => q).ToList();

            for (int i = 0; i < lStr.Count; i++)
            {
                if (tempL.Contains(lStr[i].Trim()) == false)
                {
                    tempL.Add(lStr[i].Trim());
                    lBox.Items.Add(lStr[i].Trim());
                }
            }
        }

    }
}

/*
====================== za zapis v faila za dom5 ===============

System.IO.File.WriteAllText(@"D:\stDom\names.txt", string.Empty);

string[] names1 = { "Абаджиев", "Аврамов", "Агура", "Андреев", "Арабаджиев", "Арнаудов", "Арнаудова", "Асенов", "Атанасов", "Атанасова", "Бальови", "Богоев", "Божинов", "Бонев", "Борисов", "Бояджиев", "Ванчев", "Вачков", "Велешки", "Велчев" };
string[] names2 = { "Венедикови", "Виденов", "Витяков", "Вълканов", "Вълчанов", "Вълчев", "Гемеджиев", "Георгиев", "Георгиева", "Герасимов", "Гоцев", "Гошев", "Григориев", "Григоров", "Груев", "Грънчаров", "Гълъбов", "Гюзелев", "Дамянов", "Дерменджиев" };
string[] names3 = { "Димитров", "Димитрова", "Добрев", "Дочев", "Драганов", "Дюлгеров", "Дянков", "Ерменков", "Желев", "Жендов", "Живков", "Иванов", "Иванова", "Ивков", "Игнатов", "Измирлиев", "Инджов", "Йорданов", "Йосифов", "Каменов" };
string[] names4 = { "Кантарджиев", "Каравелов", "Караджов", "Караславов", "Карастоянов", "Касабов", "Кендеров", "Кескинов", "Ковачев", "Коджа", "Колчагови", "Константинов", "Котев", "Кочев", "Кръстев", "Кушев", "Куюмджиев", "Кънев", "Кьосев", "Лефтеров" };
string[] names5 = { "Мавродиев", "Марангозов", "Маринов", "Минков", "Минов", "Минчев", "Митев", "Митов", "Михайлов", "Михайлова", "Михайловски", "Михов", "Младенов", "Москов" };
string[] names6 = { "Налбантов", "Николов", "Обрешков", "Оракчиев", "Орлов", "Панайотов", "Паница", "Панчев", "Петков", "Петров", "Петрова", "Пешев", "Плачков", "Попов", "Попова", "Първанов" };
string[] names7 = { "Радев", "Радославов", "Русев", "Русева", "Самарджиеви", "Семерджиев", "Симеонов", "Соколов", "Солаков", "Спасов", "Ставрев", "Стайков", "Стамов", "Станев", "Станишев", "Стойков", "Стойчев", "Стоянов", "Стоянова", "Събев" };
string[] names8 = { "Такев", "Танев", "Танчев", "Терзиев", "Тодоров", "Томов", "Тончев", "Тошев", "Трифонов", "Туджаров", "Тунчев", "Фучеджиев", "Хаджиев", "Хитров", "Христов", "Цветанов", "Цветков", "Цеков" };
string[] names9 = { "Цветанов", "Цветков", "Цеков", "Чакъров", "Червенков", "Чернев", "Чешмеджиев", "Шалдеви", "Шейтанов", "Щърбанов", "Яворов" };
using (StreamWriter sw = File.AppendText(@"D:\stDom\names.txt"))
{

    int counter = 0;

    for (int i = 0; i < names1.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names1[i]);
    }
    for (int i = 0; i < names2.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names2[i]);
    }
    for (int i = 0; i < names3.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names3[i]);
    }
    for (int i = 0; i < names4.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names4[i]);
    }
    for (int i = 0; i < names5.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names5[i]);
    }
    for (int i = 0; i < names6.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names6[i]);
    }
    for (int i = 0; i < names7.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names7[i]);
    }
    for (int i = 0; i < names8.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names8[i]);
    }
    for (int i = 0; i < names9.Length; i++)
    {
        counter++;
        string str1;
        string str2;

        if (counter <= 9) { str1 = counter.ToString() + "00"; }
        else if (counter <= 99) { str1 = counter.ToString() + "0"; }
        else { str1 = counter.ToString(); }

        if (i <= 9) { str2 = i.ToString() + "0"; }
        else { str2 = i.ToString(); }

        sw.WriteLine(str1 + str2 + str1 + str2);
        sw.WriteLine(names9[i]);
    }


}

*/