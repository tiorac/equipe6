class Trainer:
    from classifier.functions import Helpers    
    from classifier.persist import Persist
    from sklearn.preprocessing import StandardScaler    
    from sklearn.feature_extraction.text import TfidfVectorizer
    from sklearn.neural_network import MLPClassifier
    import pandas as pd

    def __init__(self, scaler=None, vectorizer=None):
        if (scaler == None):
            self.__scaler = self.StandardScaler()    
        else:
            self.__scaler = scaler

        self.__helpers = self.Helpers()
        if (vectorizer == None):
            self.__tfidf_vectorizer = self.TfidfVectorizer(analyzer=self.__helpers.clean_text)
        else:
            self.__tfidf_vectorizer = vectorizer



    def loadData(self, file, columns, sep='\t'):
        self.data = self.pd.read_csv("FrasesViagens2.tsv", sep='\t')
        self.data.columns = columns
    
    def preprocess(self, body_feature, label_feature):
        #Preprocessar dados:        
        #   adicionando features novas, 
        #self.data = None ### Chamar add_features do modulo Helper
        self.loadData("FrasesViagens2.tsv", [label_feature, body_feature])
        self.__helpers.add_features(self.data)
        
        #   vetorizando, 
        self.__tfidf = self.__tfidf_vectorizer.fit_transform(self.data['body_text']) ## Chamar o fit_transform do Vectorizer
        
        #   removendo coluna do texto
        df_dropped = self.data.drop(body_feature, axis=1, inplace=False)
        df_dropped.drop(label_feature, axis=1, inplace=True)

        #   Montando dataset com feature necessárias apenas
        vectorWords  = self.pd.DataFrame(self.__tfidf.toarray())
        vectorWords.columns = self.__tfidf_vectorizer.get_feature_names()

        self.X_features = self.pd.concat([df_dropped, vectorWords], axis=1)


    def train_test(self, label_feature, solver='lbfgs', alpha=1e-5, hidden_layers=(5,3), test_size=0.2):
        # Treinar e testar o modelo
        from sklearn.model_selection import train_test_split

        #   Padronizar modelo (Scaler)
        
        X_scaled = self.__scaler.fit_transform(self.X_features) ## Chamar fit_transform do Scaler

        #   Dividir modelo entre teste e traino
        X_train, X_test, y_train, y_test = train_test_split(X_scaled, self.data[label_feature], test_size=test_size) #Chamar train_test_split
        
        #   Instancia o MLPClassifier
        self.__classifier =self.MLPClassifier(solver=solver, alpha=alpha, hidden_layer_sizes=hidden_layers, random_state=None)
        self.__classifier.fit(X_train, y_train)
        self.__classifier.predict(X_test)

        print("Score: {}".format(str(self.__classifier.score(X_test,y_test))))
    
    def save_model(self, output):
        persist = self.Persist()
        persist.save(self.__tfidf_vectorizer, self.__classifier, self.__scaler, "mlp")