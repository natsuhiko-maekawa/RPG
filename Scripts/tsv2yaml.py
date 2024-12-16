import sys

import pandas as pd
import yaml

tsv_name = sys.argv[1]
df = pd.read_csv("source/tsv/" + tsv_name, sep="\t")
json_data = df.to_dict(orient="records")

filename = tsv_name.split(".")[0]
with open(f"output/yaml/{filename}.yaml", "w", encoding="utf-8") as f:
    yaml.dump(json_data, f, default_flow_style=False)
