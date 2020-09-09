import os

class Persist:
    from joblib import dump as __dump
    from sklearn.feature_extraction.text import TfidfVectorizer
    import pickle    

    def __init__(self):
        pass

    def save(self, vectorizer, classifier, scaler, filename):
        self.pickle.dump(classifier, open(filename+".classifier", "wb"))
        self.pickle.dump(vectorizer, open(filename+".vectorizer", "wb"))
        self.pickle.dump(scaler, open(filename+".scaler", "wb"))

    def load(self, filename):
        if (os.path.isfile(filename+".vectorizer")):
            vectorizer = self.pickle.load(open(filename+".vectorizer", "rb"))
            classifier = self.pickle.load(open(filename+".classifier", "rb"))
            scaler = self.pickle.load(open(filename+".scaler", "rb"))
            return (vectorizer, classifier, scaler)

        return (None, None, None)
