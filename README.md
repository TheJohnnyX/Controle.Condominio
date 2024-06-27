## Sistema de Controle de Condomínio

**Resumo**

Este documento descreve o sistema de controle de condomínios, sua finalidade, funcionalidades, microsserviços, entidades, requisitos funcionais e outras detalhes relevantes.

**Propósito**

O sistema visa auxiliar na gestão de condomínios, oferecendo funcionalidades para cadastro de unidades habitacionais, moradores, taxas condominiais.

**Usuários**

* **Administradores do condomínio:** Responsáveis pela gestão geral do condomínio, incluindo a utilização do sistema para realizar cadastros, edições, exclusões, consultas e geração de relatórios.
* **Funcionários:** Auxiliam os administradores na utilização do sistema, realizando tarefas de acordo com suas permissões de acesso.

**Microsserviços**

O sistema é composto por microsserviços modulares e independentes, cada um com uma função específica:

* **Microsserviço de Residências:** Gerencia as unidades habitacionais do condomínio, incluindo cadastro, edição, exclusão e listagem.
* **Microsserviço de Moradores:** Gerencia os moradores do condomínio, incluindo cadastro, edição, exclusão e listagem.
* **Microsserviço de Taxas de Condomínio:** Gerencia as taxas condominiais, incluindo cadastro, edição, exclusão.

**Entidades**

As entidades básicas do sistema são:

* **Residência:** Possui identificador único (ID), endereço e morador atual.
* **Morador:** Possui identificador único (ID), nome e CPF.
* **Taxa de Condomínio:** Possui identificador único (ID), valor, morador associado e residência associada.

**Requisitos Funcionais**

**Funcionalidades de Cadastro**

* **Cadastro de Residências:** Permite registrar novas unidades habitacionais, informando endereço e morador atual.
* **Cadastro de Moradores:** Permite registrar novos moradores, informando nome e CPF.
* **Cadastro de Taxas de Condomínio:** Permite cadastrar novas taxas condominiais, informando valor, data de vencimento e residência associada.

**Funcionalidades de Edição**

* **Edição de Residências:** Permite atualizar informações de uma residência cadastrada.
* **Edição de Moradores:** Permite informações de um morador cadastrado.
* **Edição de Taxas de Condomínio:** Permite atualizar informações de um condomínio cadastrada.

**Funcionalidades de Exclusão**

* **Exclusão de Residências:** Permite excluir uma residência cadastrada, informando seu identificador único (ID).
* **Exclusão de Moradores:** Permite excluir um morador cadastrado, informando seu identificador único (ID).
* **Exclusão de Taxas de Condomínio:** Permite excluir uma taxa condominial cadastrada, informando seu identificador único (ID).

**Funcionalidades de Listagem**

* **Listar Residências:** Fornece uma lista completa de todas as unidades habitacionais cadastradas, incluindo seus identificadores, endereços e moradores atuais.
* **Listar Moradores:** Fornece uma lista completa de todos os moradores cadastrados, incluindo seus identificadores, nomes e CPFs.
* **Listar Taxas de Condomínio:** Fornece uma lista completa de todas as taxas condominiais cadastradas, incluindo seus identificadores, valores, datas de vencimento e residências associadas.

**Observações**

* O sistema pode ser adaptado para atender às necessidades específicas de cada condomínio.
* A funcionalidade de pagamento de cobranças é opcional e pode ser integrada a um sistema de pagamento externo.
* O relatório de cobranças pode ser personalizado para incluir diferentes campos e filtros.

* O protótipo não atingiu o objetivo final. Apenas foi possível construir os 3 microsserviços, sem realizar as transações, conforme solicitado.

**Equipe**

* [João Carlos](https://github.com/TheJohnnyX)
* [João Paulo](https://github.com/joaopaulomts)
* [Roger](https://github.com/rogerbertan)
