import openai
#import os
#import pandas as pd
import time
import sys

# Setarea cheii de API OpenAI
openai.api_key = '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'

# Definirea funcției pentru obținerea completării de la API-ul OpenAI
def get_completion(ingredients, model="gpt-3.5-turbo"):
    # Construirea promptului utilizând variabilele primite de la utilizator
    prompt = f"Genereaza o reteta in limba engleza, cu tot cu pasii de preparare si lista completa de ingrediente, folosind urmatoarele ingrediente: "
    prompt += ", ".join(ingredients) 

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
    if len(sys.argv) < 3:
        print("Please go back and enter more than 2 ingredients.")
        sys.exit(1)

    # Obține ingredientele din argumentele liniei de comandă
    ingredients = sys.argv[1:]

    # Obține reteta generată și afișează-o
    response = get_completion(ingredients)
    print(response)