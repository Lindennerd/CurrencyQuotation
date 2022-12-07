-- Questão 1

select s.dsStatus as status, count(*) as processos
from tb_Processo as p
    inner join tb_Status as s on p.idStatus = s.idStatus
group by s.idStatus


-- Questão 2

SELECT nroProcesso, MAX(a.dtAndamento)
FROM tb_Processo as p
    INNER JOIN tb_Andamento as a on p.idProcesso = a.idProcesso
WHERE p.dtEncerramento = 2013
GROUP BY nroProcesso;

-- Questão 3

select dtEncerramento, count(dtEncerramento)
from tb_Processo
where COUNT(dtEncerramento) > 5
GROUP BY dtEncerramento

-- Questão 4

SELECT FORMAT(nroProcesso, 000000000000)
from tb_Processo