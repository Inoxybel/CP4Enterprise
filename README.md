# CP4 - Enterprise

Exercício de Checkpoint para a matéria de Enterprise Application Development voltado a C#

## Solicitação:

Checkpoint 4 – C# – Parte II

• Esta parte do checkpoint poderá ser desenvolvida em grupo (não precisar ser o grupo do
Challenge, contendo de 3 a 6 integrantes);
• A entrega será feita via Teams com envio do arquivo diretamente ao professor (chat direto),
não via chat da disciplina;
• Entregue o projeto em formato .zip, somente um integrante por grupo, sendo que neste
arquivo deve conter a pasta toda do projeto, incluindo o Solution e as pastas Debug e Release;
• O projeto deve ser implementado via Visual Studio 2022, não usar VS Code;
• Junto do .zip, entregue um arquivo .txt com o nome de todos os integrantes do grupo.

Desenvolva uma aplicação Console App que possa armazenar em memória os
funcionários de uma empresa. Essa empresa possui dois tipos de funcionários: CLT e PJ. Um
funcionário no regime CLT deve conter um código de registro, nome, gênero, salário e se
possui ou não cargo de confiança. Já o funcionário PJ deve conter um código de registro, nome,
gênero, valor hora, quantidade de horas contratada e número do CNPJ da empresa.
O sistema deve calcular o custo total mensal do colaborador para a empresa, para
o funcionário CLT, sendo que o cálculo corresponde ao valor do salário somado a 11,11% da
fração de férias, 8,33% da fração de 13o salário, 8% de FGTS, 4% de FGTS/Provisão de multa
para rescisão e 7,93% de previdenciário. Já para o funcionário PJ o valor será o valor hora
vezes a quantidade de horas contratada. O funcionário PJ também deve ter uma funcionalidade
que calcula o Custo Total mensal acrescido de horas extras que o colaborador pode ter
realizado no mês.
O sistema deve ser capaz de receber os dados dos funcionários e armazená-los
em memória, para depois realizar as seguintes operações, de acordo com a opção escolhida
pelo usuário:

• Exibir todos os dados de todos os funcionários CLT.
• Exibir todos os dados de todos os funcionários PJ.
• Exibir a soma do custo total mensal de todos os funcionários (incluindo Funcionário PJ
sem horas extras).
• Aumentar o salário de um funcionário CLT em % do salário atual, sendo que o aumento
pode ser realizado informando o número de registro do funcionário.
• Aumentar o salário de um funcionário PJ em R$ do valor hora, sendo que o aumento
pode ser realizado informando o número de registro do funcionário.
• Pesquisar um funcionário pelo registro e exibir todos os seus dados.
• Pesquisar um funcionário pelo registro e exibir o custo total mensal dele para a empresa.

O sistema não deve permitir o cálculo de horas extras ou o aumento do salário com
valores negativos.
