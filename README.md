# Felli

### Grupo 03: 
|Nome|Número|GitHub|
|:-:|:-:|:-:|
|Luiz Santos|a21901441|JundMaster|
|Pedro Marques|a21900253|pmarques93|
|Gonçalo Verde|a21901395|MrVerdinsky|

---
### Tarefas realizadas no exercício

|Gonçalo Verde|Luiz Santos|Pedro Marques|
|:-:|:-:|:-:|
||Add - Movement Input|Add - Board, Position, Renderer, Game, Player classes|
||Add - Movement Input / Select Player Input / Renderer - Messages Print|Add - Renderer - Possible Plays + Player Pieces|
||Add - Input class| Add - Input class|
|Introduction|Add - Select Pieces Input|Add - Movement Limitation / Eating Movement|
|Add - Victory |Fix - Eating Movement / Movement Limitation||
||Fix - General Inputs / Error Messages / Add - Try-Catch Movement|Fix - Victory|

---
### Repositório git
https://github.com/pmarques93/FelliProjeto2.git

---
### Descrição da solução
Para resoução do problema começámos por criar várias classes relativas ao jogo. Criámos a classe _Position_ que está diretamente ligada à classe _Board_. Estas classes são responsáveis pela criação do tabuleiro, sendo auxiliadas pela classe _Renderer_ para imprimir o tabuleiro para a consola. De seguida criámos a classe _Game_ que vai conter o _gameloop_ e o código relativo à criação de instâncias do tabuleiro e dos restantes elementos.

Após termos o tabuleiro criado com as devidas posições, criámos a classe _Player_ que vai conter os vários estados de cada peça. Esta classe vai servir para definir estados como a posição, nome, jogador selecionado e se está vivo ou não. Depois destes passos, decidimos criar uma classe específica para o _input_. Esta, vai ser responsável por todo o _input_ do utilizador, relativo aos movimentos e escolha das peças.

Após as classes cruciais para o funcionamento estarem criadas, começámos a desenvolver o _input_ e o movimento, de modo a que a peça que "come", obtenha a posição do _input_ e elimine a peça adversária. Entretanto começámos a desenvolver as condições de vitória, que vão passar pela confirmação da possibilidade de jogar de um dos jogadores.

#### Resumo de lógica implementada (algoritmos não triviais)
Durante o Run(), após o _input_ do utlizador, é chamado o método _BoardOccupied_ para sabermos se a posição desejada estão livre ou ocupada. Caso a posição estiveja livre, na possibilidade de existir a movimentação, através do método _Movement_, a peça movimenta-se. Caso contrário, se a posição estiver ocupada utilizamos o método _Eat_, que vai chamar vários métodos da mesma classe, que, por sua vez, vão confirmar se existe a possibilidade de movimentar a peça duas posições, sendo que, se movimentar a peça vai eliminar a peça adversária que estava no caminho.

No programa, utilizamos algumas vezes métodos criados, denominadamente o FreeSpace() e o OccupySpace(). Esta solução foi implementada de modo a _libertarmos_ e _ocuparmos_ espaços no tabuleiro sempre que necessitássemos, para controlar as peças dos jogadores.

Para a condição de vitória, no método _cantMove_, utilizámos _loops_ que vão confirmar quantas/quais peças do jogador ainda estão vivas, seguindo-se do método _GameOver_ que vai confirmar quais das peças analisadas não têm movimentos possíveis. Caso o número de confirmações seja o mesmo para ambos os casos, quer dizer que não existem jogadas possíveis e o jogo acaba.

---
### Arquitetura do código 
#### Classes: 
- #### _Position_;
  - Define uma posição e se essa posição pode ser jogada e se está ocupada;

- #### _Player_;
  - Define o nome, posição, se o jogador está selecionado e se está vivo;

- #### _Board_;
  - Cria o tabuleiro de jogo com posições da classe _Position_;
  
- #### _Victory_;
  - Define as condições de vitória;
  
- #### _Input_;;
  - Responsável por todo o _input_ do jogador na consola.
  
- #### _Renderer_;
  - Responsável por imprimir informação para a consola;
  
- #### _Game_;
  - Contém o _gameloop_ e o código relativo á criação de instâncias do tabuleiro e dos restantes elementos relativos ao jogo;
  
- #### _Program_;
  - Cria a _Game_ e inicia o jogo;
---
### Fluxograma

---
### Trocas de ideias/referências
#### Dúvidas gerais
- "C# documentation", _Microsoft_, Microsoft 2020,
https://docs.microsoft.com/en-us/dotnet/csharp
