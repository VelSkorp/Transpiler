using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Transpiler.Core;

namespace Transpiler
{
	public class MainViewModel : BaseViewModel
	{
		#region Public Properties

		public List<FileSystemObjectInfo> ItemsCodeInput { get; set; }

		public List<string> LanguagesItems { get; set; }

		public List<string> PatternsReaderItems { get; set; }

		public List<FileSystemObjectInfo> ItemsPatternsInput { get; set; }

		public FileSystemObjectInfo SelectedCodeInputItem { get; set; }

		public FileSystemObjectInfo SelectedPatternsInputItem { get; set; }

		public string Code { get; set; } = "";

		public string CodeToWrite { get; set; }

		public string SelectedLanguages { get; set; }
		
		public string SelectedPatternsReader { get; set; }

		#endregion

		#region Commands

		public ICommand ReadCommand { get; set; }

		public ICommand WriteCommand { get; set; }

		public ICommand TranslateCommand { get; set; }

		#endregion

		#region Constructor

		public MainViewModel()
		{
			ItemsCodeInput = new List<FileSystemObjectInfo>();
			ItemsPatternsInput = new List<FileSystemObjectInfo>();

			LanguagesItems = new List<string>
			{
				"CToPascal",
				"PascalToC",
			};

			PatternsReaderItems = new List<string>
			{
				"Json",
			};

			var drives = DriveInfo.GetDrives();
			DriveInfo.GetDrives().ToList().ForEach(drive =>
			{
				ItemsCodeInput.Add(new FileSystemObjectInfo(drive));
				ItemsPatternsInput.Add(new FileSystemObjectInfo(drive));
			});

			ReadCommand = new RelayCommand(Read);
			WriteCommand = new RelayCommand(Write);
			TranslateCommand = new RelayCommand(Translate);
		}

		#endregion

		#region Command Methods

		public void Read()
		{
			var streamReader = new StreamReader(SelectedCodeInputItem.FileSystemInfo.FullName);

			Code = streamReader.ReadToEnd();
			streamReader.Close();
		}

		public void Write()
		{
			string[] fileExtensions = SelectedLanguagesToFileExtensions.Convert(SelectedLanguages);
			string translatedCodeFilePath = SelectedCodeInputItem.FileSystemInfo.FullName.Replace(fileExtensions[0], fileExtensions[1]);
			var streamWriter = new StreamWriter(translatedCodeFilePath);

			streamWriter.Write(CodeToWrite);
			streamWriter.Close();
		}

		public void Translate()
		{
			string[] code = Code.Replace("\r", "\n").Split('\n');
			var patternsReader = StringToPatternsReader.Convert(SelectedPatternsReader, SelectedPatternsInputItem.FileSystemInfo.FullName);

			CoreDI.TranslatorContext.Translator = StringToTranslator.Convert(SelectedLanguages, code, patternsReader);

			CodeToWrite = CoreDI.TranslatorContext.Translator.Translate();
		}

		#endregion
	}
}