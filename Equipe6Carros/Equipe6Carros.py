import pickle
import numpy as np
import pandas as pd
from sklearn.ensemble import RandomForestRegressor
from sklearn.model_selection import train_test_split

def mesAnoValor_Ano(row):
  return int(row["MesAnoReferencia"].split('-')[0])

def mesAnoValor_Mes(row):
  return int(row["MesAnoReferencia"].split('-')[1])

print("Carrengado tabela")
tabelaFipe = pd.read_csv("TabelaFipeTudo.tsv", sep='\t', header=0)

print("Agrupando MesAno")
mesRef = tabelaFipe["MesAnoReferencia"].unique()

print("Criando coluna: Mes e Ano Ref")
tabelaFipe["MesRef"] = tabelaFipe.apply(mesAnoValor_Mes, axis=1)
tabelaFipe["AnoRef"] = tabelaFipe.apply(mesAnoValor_Ano, axis=1)
tabelaFipe["DiffAno"] = tabelaFipe["AnoRef"] - tabelaFipe["Ano"]

print("Agrupando por marcas")
grupoMarcas = tabelaFipe.groupby(["Marca", "Ano", "Combustivel", "Dolar", "IGPM", "Petroleo", "MesRef", "AnoRef", "DiffAno"])["Valor"].median().to_frame().reset_index()
grupoMarcas.sort_values(by=["Marca", "AnoRef", "MesRef", "Ano"], inplace=True)

print("Salvando colunas")
filename = 'colunas.sav'
pickle.dump(grupoMarcas["Marca"].unique(), open(filename, 'wb'))

print("Criando colunas de marcas")
marcas = pd.get_dummies(grupoMarcas["Marca"])

print("Salvando combustiveis")
filename = 'combustiveis.sav'
pickle.dump(grupoMarcas["Combustivel"].unique(), open(filename, 'wb'))

print("Criando colunas de combustiveis")
combustiveis = pd.get_dummies(grupoMarcas["Combustivel"])

print("Criando tabela valores e marcas")
valoresMarcas = grupoMarcas[["Ano", "MesRef", "AnoRef", "DiffAno"]]

print("Criando tabela de valores")
valores = grupoMarcas[["Valor"]]

print("Criando tabela com dados")
x_semIndices = pd.concat([valoresMarcas, combustiveis, marcas], axis=1)

print("Separando para treinamento")
xSI_train, xSI_test, ySI_train, ySI_test = train_test_split(x_semIndices, valores, test_size=0.35, random_state=101)

print("Treinando Random Forest")
forestComIndices = RandomForestRegressor()
forestComIndices.fit(xSI_train, ySI_train)

print("Salvando Random Forest Regressor")
filename = 'rfr.sav'
pickle.dump(forestComIndices, open(filename, 'wb'))

print("Testes")
print("Precisão do conjunto de treinamento: ", forestComIndices.score(xSI_train,ySI_train))
print("Precisão do conjunto de teste: ", forestComIndices.score(xSI_test,ySI_test))