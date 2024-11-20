import json
import sys

import pandas as pd
import yaml

yaml_path = sys.argv[1]

with open("source/yaml/" + yaml_path, "r", encoding="utf-8") as f:
    yaml_data = yaml.safe_load(f)

df = pd.DataFrame(yaml_data["itemList"])
tsv_data = df.to_csv(sep="\t", index=False, lineterminator="\n")

with open("output/tsv/output.tsv", "w", encoding="utf-8") as f:
    f.write(tsv_data)
