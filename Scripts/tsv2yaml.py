import pandas as pd
import yaml

df = pd.read_csv("source/tsv/input.tsv", sep="\t")
json_data = df.to_dict(orient="records")

with open("output/yaml/output.yaml", "w", encoding="utf-8") as f:
    yaml.dump(json_data, f, default_flow_style=False)
