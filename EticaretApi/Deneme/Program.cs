using System.Runtime.InteropServices;

string dosyauzantısı = "jpg";
string dosyaAdi = "ürünler pc ön image";  //ürünler pc ön image-1.jpg deriz
var data = Kayıtişlemi.Kayıt(dosyaAdi, dosyauzantısı);
var data1 = Kayıtişlemi.Kayıt(dosyaAdi, dosyauzantısı);
var data2 = Kayıtişlemi.Kayıt(dosyaAdi, dosyauzantısı);
var data3 = Kayıtişlemi.Kayıt(dosyaAdi, dosyauzantısı);


Console.WriteLine("-----------------");
foreach (var item in DataBase.dataBase)
{
    Console.WriteLine(item);
}
Console.ReadLine();


public static class DataBase
{
    public static List<string> dataBase;
    static DataBase()
    {
        dataBase = new List<string>();
    }
}

class Kayıtişlemi
{
    public static bool Kayıt(string dosyaAdı,string dosyauzantısı,int i=1,bool kontrol=true)
    {
        string data=dosyaAdı;
        if (kontrol)
        {
           data = Donusum.CharacterRegulator(dosyaAdı);
        }
        

        bool kontrol1 = databaseArama(data + "." + dosyauzantısı);

        if (kontrol1) //varsa
        {
            string newdata;
            if (kontrol)
            {
                 newdata = data + "-" + i;
               
            }
            else
            {
                var treindex=data.LastIndexOf("-");
                 newdata = data.Remove(treindex+1,data.Length-(treindex+1)).Insert(treindex+1,i.ToString());
            }
            Kayıt(newdata, dosyauzantısı, ++i, false);

            return false;
        }
        else //Yoksa
        {
            DataBase.dataBase.Add(data+"."+dosyauzantısı);
            return true;
        }

    }


    public static bool databaseArama(string dosyaAd)
    {
        foreach (var data in DataBase.dataBase)
        {
            if (dosyaAd == data)
            {
                return true;
            }

        }
        return false;
    }

}

public static class Donusum
{
    public static string CharacterRegulator(string name)
         => name.Replace("\"", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("=", "")
                .Replace("?", "")
                .Replace("_", "")
                .Replace(" ", "-")
                .Replace("@", "")
                .Replace("€", "")
                .Replace("¨", "")
                .Replace("~", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace(".", "-")
                .Replace("Ö", "o")
                .Replace("ö", "o")
                .Replace("Ü", "u")
                .Replace("ü", "u")
                .Replace("ı", "i")
                .Replace("İ", "i")
                .Replace("ğ", "g")
                .Replace("Ğ", "g")
                .Replace("æ", "")
                .Replace("ß", "")
                .Replace("â", "a")
                .Replace("î", "i")
                .Replace("ş", "s")
                .Replace("Ş", "s")
                .Replace("Ç", "c")
                .Replace("ç", "c")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
}

#region MyRegion

public async Task<List<(string filenme, string path)>> UploadAsync(string path, List<IFormFile> files)
{
    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
    //_webHostEnvironment.WebRootPath wwwroot konumunu veırı sonra ıcerısınde resource/product-images adresını alır 
    //Path.Combine() yöntemi, belirtilen dizin yollarını birleştirerek tek bir dize oluşturur.
    if (!Directory.Exists(Path.GetDirectoryName(uploadPath))) //ıcındekı adreste bır dosya varmı dıye bakar yoksa olusturu ıcerıde
    {
        Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)); //bu dizini olustur dedik
    }

    List<(string filenme, string path)> datas = new();

    List<bool> results = new();

    foreach (var file in files)
    {
        string extension = Path.GetExtension(file.FileName);
        string fileNewName = await FileRenameAsync(uploadPath, file.FileName, extension, path);

        bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
        datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
        results.Add(result);
    }

    if (results.TrueForAll(r => r.Equals(true))) //ıcerıdekı hepsı dogru ıse 
    {
        return datas;
    }
    return null;
    //todo Eğerki yukarıdaki if gecerlı degılse burda dosyaların sunucuda yuklenırken hata aldıgına daır uyarıcı bır exceptıon fırlatılması gerekıyor
}


string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            //_webHostEnvironment.WebRootPath wwwroot konumunu veırı sonra ıcerısınde resource/product-images adresını alır 
            //Path.Combine() yöntemi, belirtilen dizin yollarını birleştirerek tek bir dize oluşturur.
            if (!Directory.Exists(Path.GetDirectoryName(uploadPath))) //ıcındekı adreste bır dosya varmı dıye bakar yoksa olusturu ıcerıde
            {
                Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)); //bu dizini olustur dedik
            }

            List<(string filenme, string path)> datas = new();

            List<bool> results = new();

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName);
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName, extension,path);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true))) //ıcerıdekı hepsı dogru ıse 
            {
                return datas;
            }
            return null;
            //todo Eğerki yukarıdaki if gecerlı degılse burda dosyaların sunucuda yuklenırken hata aldıgına daır uyarıcı bır exceptıon fırlatılması gerekıyor
        }


public async Task<List<(string filenme, string path)>> UploadAsync(string path, List<IFormFile> files)
{
    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
    //_webHostEnvironment.WebRootPath wwwroot konumunu veırı sonra ıcerısınde resource/product-images adresını alır 
    //Path.Combine() yöntemi, belirtilen dizin yollarını birleştirerek tek bir dize oluşturur.
    if (!Directory.Exists(Path.GetDirectoryName(uploadPath))) //ıcındekı adreste bır dosya varmı dıye bakar yoksa olusturu ıcerıde
    {
        Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)); //bu dizini olustur dedik
    }

    List<(string filenme, string path)> datas = new();

    List<bool> results = new();
    foreach (var file in files)
    {
        string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

        bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
        datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
        results.Add(result);
    }

    if (results.TrueForAll(r => r.Equals(true))) //ıcerıdekı hepsı dogru ıse 
    {
        return datas;
    }
    return null;
    //todo Eğerki yukarıdaki if gecerlı degılse burda dosyaların sunucuda yuklenırken hata aldıgına daır uyarıcı bır exceptıon fırlatılması gerekıyor
}

private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
{
    string newFileName = await Task.Run(async () =>
    {
        string extension = Path.GetExtension(fileName);
        string newFileName = string.Empty;
        if (first)
        {
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            newFileName = $"{NameOperation.CharacterRegulator(oldName)}{extension}";
        }
        else
        {
            newFileName = fileName;
            int indexno1 = newFileName.IndexOf("-");
            if (indexno1 == -1)
            {
                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
            }
            else
            {
                int lastIndex = 0;
                while (true)
                {
                    lastIndex = indexno1;
                    indexno1 = newFileName.IndexOf("-", indexno1 + 1);
                    if (indexno1 == -1)
                    {
                        indexno1 = lastIndex;
                        break;
                    }
                }
                int indexno2 = newFileName.IndexOf(".");
                string fileNo = newFileName.Substring(indexno1 + 1, indexno2 - indexno1 - 1); //no 1 den basla no2-no1-1 kadar ılerıle

                if (int.TryParse(fileNo, out int _fileNo))
                {
                    _fileNo++;
                    newFileName = newFileName.Remove(indexno1 + 1, indexno2 - indexno1 - 1)
                                          .Insert(indexno1 + 1, _fileNo.ToString());
                }
                else
                {
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                }
            }
        }
        if (File.Exists($"{path}\\{newFileName}"))
        {
            return await FileRenameAsync(path, newFileName, false);
        }
        else
        {
            return newFileName;
        }
    });
    return "";
}
#endregion