import glob
import itertools
import os
from PIL import Image, ImageChops

source = "source/image/**.png"
output = "output/image"


# q=閾値, x=色
def posterization(q: int, x: tuple) -> int:
    for i in range(q):
        if (256 / q * i <= x) & (256 / q * (i + 1) > x):
            x = 256 / (q-1) * i
    return int(x)


def edge(image: Image, width: int, height: int) -> Image:
    alpha = Image.new('RGBA', (width, height))
    black = Image.new('RGBA', (width, height), (0, 0, 0))
    silhouette = Image.composite(black, alpha, image)
    canvas = alpha

    for i, j in itertools.product([-1, 0, 1], [-1, 0, 1]):
        offset = ImageChops.offset(silhouette, i, j)
        canvas = Image.alpha_composite(canvas, offset)

    canvas = Image.alpha_composite(canvas, image)
    return canvas


def main():
    p = 2  # 縮小
    q = 8  # ポスタリゼーション
    r = True  # 縁取り
    s = False  # 拡大

    files = glob.glob(source)
    for file in files:
        img = Image.open(file)  # 画像の読み込み
        width, height = img.size  # 画像サイズを取得
        img2 = Image.new('RGBA', (int(width / p), int(height / p)))  # 取得したサイズと同じ空のイメージを新規に作成

        for x in range(width):
            for y in range(height):
                r, g, b, a = img.getpixel((x, y))  # ピクセルを取得

                if a >= 128:
                    a = 255
                else:
                    a = 0

                r = posterization(q, r)
                g = posterization(q, g)
                b = posterization(q, b)

                if x % p == 0 and y % p == 0:  # モザイク化
                    img2.putpixel((int(x / p), int(y / p)), (r, g, b, a))
        if r:
            img2 = edge(img2, int(width / p), int(height / p))
        if s:
            img2 = img2.resize((width, height), Image.BOX)  # 拡大
        img2.save(os.path.join(output, os.path.basename(file)))


if __name__ == '__main__':
    main()
