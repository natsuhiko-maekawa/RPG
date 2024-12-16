import sys

import pandas as pd
import yaml

yaml_name = sys.argv[1]
with open("source/yaml/" + yaml_name, "r", encoding="utf-8") as f:
    df = pd.json_normalize(yaml.safe_load(f))

filename = yaml_name.split(".")[0]

df.to_csv(f"output/tsv/{filename}.tsv", sep="\t", index=False)
