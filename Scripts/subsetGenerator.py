import glob

import yaml

files = glob.glob("../Assets/ScriptableObject/*ViewScriptableObject.asset", recursive=True)
message_file = "../Assets/ScriptableObject/MessageScriptableObject.asset"


def read_scriptable_object(file: str) -> dict:
    with open(file, "r", encoding="utf-8") as f:
        yaml_data = "\n".join(f.readlines()[14:])
        item_list: dict = yaml.safe_load(yaml_data)
    return item_list


str_list = list()
for file in files:
    item_list = read_scriptable_object(file)
    tmp_str_list = [item["name"] for item in item_list["itemList"] if item["name"]]
    str_list.extend(tmp_str_list)

message_list = read_scriptable_object(message_file)
tmp_str_list = ["".join(message["message"]) for message in message_list["itemList"] if message["message"]]
str_list.extend(tmp_str_list)

numbers = "".join([str(number) for number in (range(0, 10))])
others = "計たち"

# いったん一つの文字列に変換したのち、distinct、sortを行い、再び文字列化
# その後、空白文字で分割し、再び結合
subset = "".join(str_list) + numbers + others
subset = "".join(sorted(set(subset)))
subset = "".join(subset.split())

with open("output/txt/subset.txt", "w", encoding="utf-8") as f:
    f.write(subset)
