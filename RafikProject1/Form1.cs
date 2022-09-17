using System.Text.RegularExpressions;

namespace RafikProject1
{
    public partial class Form1 : Form
    {
        public List<string> dataTypes = new List<string>() { "",
                                                             "Короткий текст",
                                                             "Длинный текст",  
                                                             "Числовой",
                                                             "BigInt",
                                                             "Дата и время",
                                                             "Денежный",
                                                             "Счётчик",
                                                             "Логический",
                                                             "Поле объекта OLE",
                                                             "Гиперссылка" };

        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            DataType.DataSource = dataTypes;
            saveFileDialog.Filter = "MaxTableType(*.mtt)|*.mtt";
            openFileDialog.Filter = "MaxTableType(*.mtt)|*.mtt|All files(*.*)|*.*";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;

            string path = saveFileDialog.FileName;
            string textToWrite = "";

            textToWrite += TableNameTextBox.Text + ";\r";

            for(int i = 0; i < Grid.RowCount - 1; i++)
            {
                textToWrite += Grid.Rows[i].Cells[0].Value + ";";
                textToWrite += Grid.Rows[i].Cells[1].Value + ";";
                textToWrite += Grid.Rows[i].Cells[2].Value + ";\r";
            }

            File.WriteAllText(path, textToWrite);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;

            string fileText = File.ReadAllText(openFileDialog.FileName);
            fileText = Regex.Replace(fileText,@"[\r]","");
            string[] concatText = fileText.Split(";");

            TableNameTextBox.Text = concatText[0];
            Grid.Rows.Clear();

            for(int pointer = 1; pointer < concatText.Count() - 1; pointer += 3)
            {
                Grid.Rows.Add(concatText[pointer], concatText[pointer + 1], concatText[pointer + 2]);
            }
        }
    }
}