import sys
import openai

# Ensure you have your Azure OpenAI API key and endpoint set up
api_key = "971ec1b405084f5c9fe4da9d135c4cae"
endpoint = "https://becopenaidev6.openai.azure.com/"

# Function to call Azure OpenAI
def get_code_review_comments(diff):
    openai.api_key = api_key
    response = openai.Completion.create(
        engine="gpt-4o",
        prompt=f"Review the following code changes and provide comments:\n\n{diff}",
        max_tokens=150
    )
    return response.choices[0].text.strip()

if __name__ == "__main__":
    diff = sys.argv[1]
    comments = get_code_review_comments(diff)
    print(comments)
