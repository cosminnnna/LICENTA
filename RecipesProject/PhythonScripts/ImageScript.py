import openai
import time
import sys

# Setarea cheii de API OpenAI
openai.api_key = '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'

# Definirea funcției pentru generarea imaginii de la API-ul OpenAI
def generate_image(recipe):
    prompt = f"Genereaza o imagine cu acest preparat, proaspat pregatit, cat mai apetisant: {recipe}"
    response = openai.Image.create(
        model="dall-e-3",
        prompt=prompt,
        n=1,
        size="1024x1024"
    )
    return response['data'][0]['url']


if __name__ == "__main__":
    # Obține URL-ul imaginii generate și afișează-l
    recipe = sys.argv[1:]
    image_url = generate_image(recipe)
    #print("IMAGE")
    print(image_url)