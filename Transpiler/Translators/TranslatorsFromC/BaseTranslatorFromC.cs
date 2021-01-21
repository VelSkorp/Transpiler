using System.Collections.Generic;
using System.Text;

namespace Transpiler
{
    public class BaseTranslatorFromC : BaseTranslator
    {
        protected Dictionary<string, List<string>> Variables { get; set; }

        public BaseTranslatorFromC(string codeFilePath, string patternsFilePath)
            : base(codeFilePath, patternsFilePath)
        {
            DataTypes = new string[] { "char ", "unsigned char ", "signed char ", "int ", "unsigned int ", "signed int ", "short int ",
                "unsigned short int ", "signed short int ", "long int ", "unsigned long int ", "signed long int ", "float ", "double ", "long double " };

            Variables = ParseVariables();
        }

        public override string Translate()
        {
            return base.Translate();
        }

        public Dictionary<string, List<string>> ParseVariables()
        {
            var variables = new Dictionary<string, List<string>>();
            int i = 0;

            while (i < Code.Length)
            {
                bool deleteRow = false;
                string codeRow = Code[i];

                foreach (var dataType in DataTypes)
                {
                    if (codeRow.Contains(dataType))
                    {
                        deleteRow = ParseVariableName(codeRow, dataType, ref variables);

                        break;
                    }
                }

                if (deleteRow)
                {
                    Code[i] = "\r";
                }

                i++;
            }

            return variables;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeRow"></param>
        /// <param name="dataType"></param>
        /// <param name="variables"></param>
        /// <returns>Returns true if code nust be delete</returns>
        public bool ParseVariableName(string codeRow, string dataType, ref Dictionary<string, List<string>> variables)
        {
            int typeIndex = codeRow.IndexOf(dataType);
            typeIndex += dataType.Length;

            var varibleName = new StringBuilder();
            bool deleteRow = false;

            for (int j = typeIndex; j < codeRow.Length; j++)
            {
                if (codeRow[j] == ' ')
                {
                    break;
                }

                if (codeRow[j] == ';')
                {
                    deleteRow = true;
                    break;
                }

                varibleName.Append(codeRow[j]);
            }

            if (variables.ContainsKey(dataType))
            {
                variables[dataType].Add(varibleName.ToString());
            }
            else
            {
                variables.Add(dataType, new List<string> { varibleName.ToString() });
            }

            return deleteRow;
        }
    }
}