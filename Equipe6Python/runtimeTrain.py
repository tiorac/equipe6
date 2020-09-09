from classifier.train import Trainer


trainer = Trainer()
trainer.preprocess('body_text', 'label')
trainer.train_test('label')
trainer.save_model("")