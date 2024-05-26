# RE4-ETM-TOOL
Extract and repack RE4 ETM files (RE4 ubisoft/2007/steam/uhd/Ps2)

**Translate from Portuguese Brazil**

Programa destinado a extrair e reempacotar arquivos .ETM
<br> Ao extrair será gerado um arquivo de extenção .idxetm, ele será usado para o repack.

## Extract

Exemplo:
<br>RE4_ETM_TOOL.exe "r10a_10.ETM"

! Ira gerar um arquivo de nome "r10a_10.idxetm";
<br>! Ira criar uma pasta de nome "r10a_10";
<br>! Na pasta vão conter os arquivos que estavam dentro do ETM;

## Repack

Exemplo:
<br>RE4_ETM_TOOL.exe "r10a_10.idxetm"

! No arquivo .idxetm vai conter os nomes dos arquivos que vão ser colocados no ETM;
<br>! Os arquivos têm que estar em uma pasta do mesmo nome do idxetm. Ex: "r10a_10";
<br>! No arquivo .idxetm as linhas iniciadas com um dos caracteres **# / \\ :** são consideradas comentários e não arquivos;
<br>! O nome do arquivo gerado é o mesmo nome do idxetm, mas com a extenção .etm;

**At.te: JADERLINK**
<br>2024-05-25