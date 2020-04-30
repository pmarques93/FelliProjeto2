# Felli

### Grupo 03: 
|Nome|Número|GitHub|
|:-:|:-:|:-:|
|Luiz Santos|a21901441|JundMaster|
|Pedro Marques|a21900253|pmarques93|
|Gonçalo Verde|a21901395|MrVerdinsky|

---
### Tarefas realizadas no exercício

Tarefas realizadas por ordem cronológica
|Gonçalo Verde|Luiz Santos|Pedro Marques|
|:-:|:-:|:-:|

---
### Repositório git
https://github.com/pmarques93/FelliProjeto2.git

---
### Descrição da solução
Para resoução do problema começámos por criar várias classes relativas ao jogo. Criámos as classes _State_ e _Position_ que estão diretamente ligadas à classe _Board_. Estas três classes são responsáveis pela criação do tabuleiro, sendo auxiliadas pela classe _Renderer_ para imprimir o tabuleiro para a consola. De seguida criámos a classe _Game_ que vai conter o _gameloop_ e o código relativo á criação de instâncias do tabuleiro e dos restantes elementos.

---
### Arquitetura do código
#### Métodos:
- #### _Main()_;
  - O método corre a classe _Game_;
  
#### Classes:
- #### _State_;
  - Define se algo é verdadeiro ou falso. Neste caso, define o estado da posição no tabuleiro;
  
- #### _Position_;
  - Contém a _Row_, _Column_, que vão definir a posição no tabuleiro;

- #### _Board_;
  - Cria o tabuleiro de jogo;
  - Contém variáveis _Position_ e _State_;
  
- #### _Renderer_;
  - Responsável por imprimir informação para a consola;
  
- #### _Game_;
  - Contém o _gameloop_ e o código relativo á criação de instâncias do tabuleiro e dos restantes elementos.;
  
---
### Fluxograma

---
### Trocas de ideias/referências

