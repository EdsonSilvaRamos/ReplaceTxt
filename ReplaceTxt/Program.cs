using System.Text.RegularExpressions;

Console.WriteLine("Copie e cole o diretório que contém os arquivos TXT com valores nesse modelo: XX.XX XXX");
var caminhoDoArquivo = Console.ReadLine();

if (!string.IsNullOrEmpty(caminhoDoArquivo))
{   
    var arquivosTxt = Directory.GetFiles(caminhoDoArquivo, "*.txt");

    if (arquivosTxt.Length > 0)
    {
        foreach (var arquivoTxt in arquivosTxt)
        {
            var conteudoArquivo = File.ReadAllText(arquivoTxt);
            var regex = new Regex(@"([0-9]{2}[\.][0-9]{2}[ ][0-9]{3})", RegexOptions.IgnoreCase);
            var combinacoesEncontradas = regex.Matches(conteudoArquivo);

            var mostrarMensagem = false;

            if (combinacoesEncontradas.Count > 0)
            {
                for (int i = 0; i < combinacoesEncontradas.Count; i++)                
                {
                    var textoEncontrado = combinacoesEncontradas[i].Value;
                    var textoEncontradoAlterado = textoEncontrado.Replace(" ", ";");
                    conteudoArquivo = conteudoArquivo.Replace(textoEncontrado, textoEncontradoAlterado);
                }

                mostrarMensagem = true;
            }

            if (mostrarMensagem)
            {
                var arquivo = Path.GetFileName(arquivoTxt);
                Console.WriteLine("Arquivo: " + arquivo + " alterado.");

                var caminhoSalvar = Path.GetFullPath(arquivoTxt);
                Console.WriteLine("Salvando...");
                File.WriteAllText(caminhoSalvar, conteudoArquivo);
            }
        }

        Console.WriteLine("Fim do processo...");
    }
    else
    {
        Console.WriteLine("Não foram encontrados arquivos TXT");
    }
}
else
{
    Console.WriteLine("Diretório não informado!");
}

Console.WriteLine("Digite qualquer tecla para sair...");
Console.ReadLine();

