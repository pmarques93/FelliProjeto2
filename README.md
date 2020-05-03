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
||Game - Movement Input|Add - Board, Position, Renderer, Game, Player classes|
||Movement Input / Select Player Input / Renderer - Messages Print|Renderer - Possible Plays + Player Pieces|
||Add - Input class| Add - Input class|
||Select Pieces Input|Limited Movement|
|Add - Victory Class / Win Checker|Eating Movement - Try-Catch|Eating Movement|

---
### Repositório git
https://github.com/pmarques93/FelliProjeto2.git

---
### Descrição da solução
Para resoução do problema começámos por criar várias classes relativas ao jogo. Criámos a classe _Position_ que está diretamente ligada à classe _Board_. Estas classes são responsáveis pela criação do tabuleiro, sendo auxiliadas pela classe _Renderer_ para imprimir o tabuleiro para a consola. De seguida criámos a classe _Game_ que vai conter o _gameloop_ e o código relativo á criação de instâncias do tabuleiro e dos restantes elementos.

Após ter o tabuleiro criado com as devidas posições, criámos a classe _Player_ que vai conter os vários estados de cada peça. Esta classe vai servir para saber informações como a posição, nome, jogador selecionado e se está vivo ou não. Depois destes passos, decidimos criar uma classe específica para o _input_. Esta, vai ser responsável por todo o _input_ do utilizador, relativo aos movimentos e escolha das peças.

---
### Arquitetura do código 
#### Classes: 
- #### _Position_;
  - Define uma posição e se essa posição pode ser jogada e se está ocupada;

- #### _Player_;
  - Define o nome, posição, se o jogador está selecionado e se está vivo;

- #### _Board_;
  - Cria o tabuleiro de jogo com posições da classe _Position_;
  
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
