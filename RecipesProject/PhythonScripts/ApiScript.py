import openai
import os
import pandas as pd
import time

# Setarea cheii de API OpenAI
#openai.api_key = 'sk-proj-BhwfasWOa85jqMZvwjXfT3BlbkFJFLgulPdqlC6fMfvEzHMW'

# Definirea funcției pentru obținerea completării de la API-ul OpenAI
#def get_completion(ingredient1, ingredient2, ingredient3, model="gpt-3.5-turbo"):
    # Construirea promptului utilizând variabilele primite de la utilizator
   # prompt = f"Genereaza o reteta, adica pasii de preparare, folosind urmatoarele ingrediente: {ingredient1}, {ingredient2}, {ingredient3}."
    
    # Generarea mesajului pentru API-ul OpenAI
    #messages = [{"role": "user", "content": prompt}]
    #response = openai.ChatCompletion.create(
     #   model=model,
      #  messages=messages,
       # temperature=0,
    #)
    
    #return response.choices[0].message["content"]
import sys

# Setarea cheii de API OpenAI
openai.api_key = 'sk-proj-BhwfasWOa85jqMZvwjXfT3BlbkFJFLgulPdqlC6fMfvEzHMW'

# Definirea funcției pentru obținerea completării de la API-ul OpenAI
def get_completion(ingredient1, ingredient2, ingredient3, model="gpt-3.5-turbo"):
    # Construirea promptului utilizând variabilele primite de la utilizator
    prompt = f"Genereaza o reteta, adica pasii de preparare, folosind urmatoarele ingrediente: {ingredient1}, {ingredient2}, {ingredient3}."

    # Generarea mesajului pentru API-ul OpenAI
    messages = [{"role": "user", "content": prompt}]
    response = openai.ChatCompletion.create(
        model=model,
        messages=messages,
        temperature=0,
    )

    return response.choices[0].message["content"]

if __name__ == "__main__":
    # Verifică dacă au fost furnizate suficiente argumente de la linia de comandă
    if len(sys.argv) < 4:
        print("Usage: python ApiScript.py ingredient1 ingredient2 ingredient3")
        sys.exit(1)

    # Obține ingredientele din argumentele liniei de comandă
    ingredient1 = sys.argv[1]
    ingredient2 = sys.argv[2]
    ingredient3 = sys.argv[3]

    # Obține reteta generată și afișează-o
    response = get_completion(ingredient1, ingredient2, ingredient3)
    print(response)
