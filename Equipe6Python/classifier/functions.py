class Helpers:
    import pandas as pd
    import string
    import nltk
    import re

    def __init__(self):
        #self.__stemmer = self.nltk.PorterStemmer()
        #self.__stopwords = self.nltk.corpus.stopwords.words('english')
        self.__stemmer = self.nltk.stem.rslp.RSLPStemmer()
        self.__stopwords = self.nltk.corpus.stopwords.words("portuguese")
        


    def clean_text(self, text):
        # 5 passos para limpeza do texto
        text2 = self.__removeVariable(text)
        text2 = "".join([word.lower() for word in text2 if word not in self.string.punctuation])
        tokens = self.re.split('\W+', text2)
        text2 = [self.__processStem(word) for word in tokens if word not in self.__stopwords]
        result = [word for word in text2 if word != None]

        return result

    def __processStem(self, word):
        if (word != None and word != ""):
            return self.__stemmer.stem(word)

    def __count_string(self, text, collection):
        # Contar quantidade de COLLECTION na frase TEXT
        pass

    def __removeVariable(self, text):
        text = text.replace("{x}", "").replace("{y}", "").replace("{z}", "")
        return text

    def __count_punct(self, text):
        text2 = self.__removeVariable(text)
        count = sum([1 for char in text2 if char in self.string.punctuation])
        return round(count/(len(text2) - text2.count(" ")), 3)*100

    def add_features(self, data):
        # Adicionar features (body_len, punct% e upper%) ao dataset
        data['body_len'] = data['body_text'].apply(lambda x: len(x) - x.count(" "))
        data['punct%'] = data['body_text'].apply(lambda x: self.__count_punct(x))
        return data

