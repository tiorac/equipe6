class Predictor:
    from sklearn.preprocessing import StandardScaler    
    from sklearn.feature_extraction.text import TfidfVectorizer
    import pandas as pd
    from classifier.functions import Helpers
    from classifier.persist import Persist
   
    def __init__(self, saved_model=None):
        self.__scaler = self.StandardScaler()    
        self.__helpers = self.Helpers()

        if (saved_model == None):
            self.__tfidf_vectorizer = self.TfidfVectorizer(analyzer=self.__helpers.clean_text)
        else:
            persist = self.Persist()
            self.__tfidf_vectorizer, self.__classifier, self.__scaler = persist.load(saved_model)

    def __vectorize(self, msg, df):
        message_vect = self.__tfidf_vectorizer.transform(msg)  
        
        df_vect = self.pd.DataFrame(message_vect.toarray())    
        df_vect.columns = self.__tfidf_vectorizer.get_feature_names()
        df_result = self.pd.concat([df.reset_index(drop=True), df_vect], axis=1)
        
        df_scaled = self.__scaler.transform(df_result)
        return df_scaled
        
    def __format_message(self, msg):
        return self.__format_messages([msg])
    def __format_messages(self, msgs):
        df = self.pd.DataFrame(msgs, columns=["body_text"])
        df = self.__helpers.add_features(df)
        df = self.__vectorize(msgs, df[['body_len','punct%']])
        return df
        
    # Predict Message
    def predict(self, message):
        vectorize_message = self.__format_message(message)

        prediction = self.__classifier.predict(vectorize_message)[0]
        probs = self.__classifier.predict_proba(vectorize_message)
        prob= [[round(x,5) for x in i] for i in probs.tolist()]

        return (prediction, prob)