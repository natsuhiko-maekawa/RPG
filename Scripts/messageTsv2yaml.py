import pandas as pd
import re
import yaml

df = pd.read_csv("source/tsv/message.tsv", sep="\t")
json_data = df.to_dict(orient="records")

for data in json_data:
    message = data["message"]
    messages = re.split(r"(\[.+?])", message)
    messages = [x for x in messages if x]
    data["message"] = messages

print(json_data)
with open("output/yaml/message.yaml", "w", encoding="utf-8") as f:
    yaml.dump(json_data, f, default_flow_style=False)
