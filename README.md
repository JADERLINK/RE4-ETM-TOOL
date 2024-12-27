# RE4-ETM-TOOL
Extract and repack RE4 ETM files (RE4 2007/PS2/UHD/PS4/NS/GC/WII/XBOX360)

**Translate from Portuguese Brazil**

Programa destinado a extrair e reempacotar arquivos .ETM
<br> Ao extrair será gerado um arquivo de extenção .idxetm, ele será usado para o repack.

**Update B.1.1.0**
<br>Adicionado suporte para as versões GC/WII/XBOX360;

**Update V.1.0.2**
<br>Nessa nova versão, para arquivos "0000.ETM", irá gerar uma pasta de nome "0000_ETM", mudança feita para evitar sobreposição de arquivos.

## Extract

Exemplo:
<br>RE4_ETM_TOOL_*.exe "r10a_010.ETM"

! Vai gerar um arquivo de nome "r10a_010.idxetm";
<br>! Vai criar uma pasta de nome "r10a_010";
<br>! Na pasta vão conter os arquivos que estavam dentro do ETM;

## Repack

Exemplo:
<br>RE4_ETM_TOOL_*.exe "r10a_010.idxetm"

! No arquivo .idxetm vai conter os nomes dos arquivos que vão ser colocados no ETM;
<br>! Os arquivos têm que estar em uma pasta do mesmo nome do idxetm. Ex: "r10a_010";
<br>! No arquivo .idxetm as linhas iniciadas com um dos caracteres **# / \\ :** são consideradas comentários e não arquivos;
<br>! O nome do arquivo gerado é o mesmo nome do idxetm, mas com a extenção .etm;

## BIG_ENDIAN vs LITTLE_ENDIAN

! Para as versões "GC/WII/XBOX360" use a tool de nome BIG_ENDIAN;
<br>! Para as versões "2007/PS2/UHD/PS4/NS" use a tool de nome LITTLE_ENDIAN;

**At.te: JADERLINK**
<br>2024-12-27