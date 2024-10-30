import sys

import pandas as pd
import yaml

tsv_path = sys.argv[1]
df = pd.read_csv("source/tsv/" + tsv_path, sep="\t")
json_data = df.to_dict(orient="records")

with open("output/yaml/output.yaml", "w", encoding="utf-8") as f:
    yaml.dump(json_data, f, default_flow_style=False)
