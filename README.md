# Felli

## Grupo 03

|Nome|Número|GitHub|
|:-:|:-:|:-:|
|Luiz Santos|a21901441|JundMaster|
|Pedro Marques|a21900253|pmarques93|
|Gonçalo Verde|a21901395|MrVerdinsky|

## Tarefas realizadas no exercício

>Por ordem cronológica

|Gonçalo Verde|Luiz Santos|Pedro Marques|
|:-:|:-:|:-:|
||Add - Movement Input|Add - Board/Position/Renderer/Game/Player classes|
||Add - Movement Input / Select Player Input|Add - Renderer - Possible Plays|
||Add - Renderer - Messages Print|Add - Renderer - Player Pieces|
||Add - Input class|Add - Input class|
|Add - Introduction|Add - Select Pieces Input|Add - Movement Limitation|
|Add - Victory|Fix - Eating Movement/ Movement Limitation|Add - Eating Movement|
||Fix - General Inputs/Error Messages/Add - Try-Catch Movement|Fix - Victory|
|XML Documentation|UML Diagram|README.md|

## Repositório git

[Repositório GitHub](https://github.com/pmarques93/FelliProjeto2.git)

## Breve descrição da solução

Para resolução do problema criámos várias classes com diferentes
responsabilidades. Criámos classes relativas ao jogo, uma classe com o propósito
de receber o _input_ do jogador e também uma classe cuja única função é imprimir
o tabuleiro/_inputs_* na consola.

### Resumo de lógica implementada (algoritmos não triviais)

Durante o _Run()_, após o _input_ do utilizador, é chamado o método
_BoardOccupied()_para sabermos se a posição desejada está livre ou ocupada.
Caso a posição esteja livre, na possibilidade de existir a movimentação,
através do método_Movement()_, a peça movimenta-se. Caso a posição esteja
ocupada, utilizamos o método _Eat_(), que vai chamar vários métodos da mesma
classe, que,por sua vez, vão confirmar se existe a possibilidade de movimentar
a peça duas posições, sendo que, se movimentar a peça vai eliminar a peça
adversária que estava no caminho.

No programa, utilizámos várias vezes métodos criados, nomeadamente o
_FreeSpace()_ e o _OccupySpace()_. Esta solução foi implementada de modo a
_libertarmos_ e _ocuparmos_ espaços no tabuleiro sempre que necessitássemos,
para controlar as peças dos jogadores.

Para a condição de vitória, no método _Gameover()_, utilizámos _loops_ que vão
confirmar quantas/quais peças do jogador ainda estão vivas, seguindo-se do
método _CantMove_()_ que vai confirmar quais das peças analisadas não têm
movimentos possíveis, quer de uma casa ou duas casas. Caso o número de
confirmações seja o mesmo para ambos os casos, quer dizer que não existem
jogadas possíveis e o jogo acaba.

### Diagrama UML

![Diagrama UML](/diagrama_uml.jpg)

### Trocas de ideias/referências

#### Dúvidas gerais

["C# documentation",_Microsoft_, Microsoft 2020](
  https://docs.microsoft.com/en-us/dotnet/csharp)
