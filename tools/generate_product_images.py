from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path

from PIL import Image, ImageChops, ImageDraw, ImageFont


ROOT = Path(__file__).resolve().parents[1]
ASSETS_DIR = ROOT / "StroyMaterials.App" / "Assets" / "Products"

WIDTH = 360
HEIGHT = 260
SCALE = 1


@dataclass(frozen=True)
class ProductImageSpec:
    article: str
    kind: str
    label: str
    accent: tuple[int, int, int]
    secondary: tuple[int, int, int] = (232, 224, 210)


SPECS: tuple[ProductImageSpec, ...] = (
    ProductImageSpec("I6MH89", "roller", "Валик", (76, 111, 149)),
    ProductImageSpec("GN6ICZ", "helmet", "Каска", (230, 126, 34)),
    ProductImageSpec("81F1WG", "helmet", "Каска", (238, 238, 230)),
    ProductImageSpec("ZR70B4", "brick", "Кирпич", (202, 112, 64)),
    ProductImageSpec("83M5ME", "brush", "Кисть", (181, 121, 59)),
    ProductImageSpec("M26EXW", "bag", "Клей", (73, 133, 169)),
    ProductImageSpec("ZKQ5FF", "blades", "Лезвия", (184, 60, 54)),
    ProductImageSpec("4WZEOT", "blades", "Лезвия", (65, 105, 153)),
    ProductImageSpec("4JR1HN", "spatula", "Шпатель", (83, 87, 93)),
    ProductImageSpec("QHNOKR", "shield", "Маска", (76, 116, 135)),
    ProductImageSpec("Z3XFSP", "knife", "Нож", (230, 138, 37)),
    ProductImageSpec("61PGH3", "glasses", "Очки", (75, 142, 114)),
    ProductImageSpec("Z3LO0U", "glasses", "Очки", (68, 107, 157)),
    ProductImageSpec("0YGHZ7", "glasses", "Очки", (82, 82, 88)),
    ProductImageSpec("EQ6RKO", "liner", "Подшлемник", (220, 226, 232)),
    ProductImageSpec("ASPXSG", "bag", "Ровнитель", (118, 100, 156)),
    ProductImageSpec("K0YACK", "bag", "ЦПС", (164, 132, 70)),
    ProductImageSpec("O43COU8", "box", "Товар", (151, 109, 72)),
    ProductImageSpec("O43COU", "bucket", "Шпаклевка", (120, 145, 170)),
    ProductImageSpec("LPDDM4", "bag", "Штукатурка", (204, 133, 62)),
    ProductImageSpec("LQ48MW", "bag", "Штукатурка", (214, 148, 68)),
)


def font(size: int, *, bold: bool = False) -> ImageFont.ImageFont:
    candidates = [
        Path("C:/Windows/Fonts/arialbd.ttf" if bold else "C:/Windows/Fonts/arial.ttf"),
        Path("C:/Windows/Fonts/calibrib.ttf" if bold else "C:/Windows/Fonts/calibri.ttf"),
    ]
    for path in candidates:
        if path.exists():
            return ImageFont.truetype(str(path), size)
    return ImageFont.load_default()


def draw_rounded_rectangle(
    draw: ImageDraw.ImageDraw,
    box: tuple[int, int, int, int],
    radius: int,
    fill: tuple[int, int, int],
    outline: tuple[int, int, int] | None = None,
    width: int = 1,
) -> None:
    draw.rounded_rectangle(box, radius=radius, fill=fill, outline=outline, width=width)


def text_center(
    draw: ImageDraw.ImageDraw,
    xy: tuple[int, int],
    text: str,
    fill: tuple[int, int, int],
    size: int = 20,
    bold: bool = True,
) -> None:
    used_font = font(size, bold=bold)
    box = draw.textbbox((0, 0), text, font=used_font)
    draw.text((xy[0] - (box[2] - box[0]) / 2, xy[1] - (box[3] - box[1]) / 2), text, fill=fill, font=used_font)


def draw_floor(draw: ImageDraw.ImageDraw) -> None:
    draw.ellipse((72, 202, 288, 231), fill=(225, 220, 211))


def draw_bag(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 116, 48
    draw.polygon([(x, y + 18), (x + 38, y), (x + 150, y + 16), (x + 150, y + 154), (x + 112, y + 174), (x, y + 154)], fill=(235, 231, 220), outline=(198, 187, 170))
    draw.polygon([(x + 112, y + 36), (x + 150, y + 16), (x + 150, y + 154), (x + 112, y + 174)], fill=(213, 205, 190), outline=(190, 179, 160))
    draw_rounded_rectangle(draw, (x + 18, y + 45, x + 106, y + 130), 8, fill=(248, 247, 240), outline=(201, 192, 177))
    draw.rectangle((x + 18, y + 45, x + 106, y + 69), fill=spec.accent)
    text_center(draw, (x + 62, y + 91), spec.label.upper(), (58, 57, 53), 15)
    draw.rectangle((x + 30, y + 142, x + 92, y + 148), fill=(188, 181, 166))


def draw_bucket(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 105, 62
    draw.ellipse((x, y, x + 150, y + 32), fill=(235, 239, 239), outline=(171, 177, 180))
    draw.rectangle((x, y + 16, x + 150, y + 126), fill=(244, 246, 245), outline=(171, 177, 180))
    draw.ellipse((x, y + 110, x + 150, y + 142), fill=(218, 224, 224), outline=(171, 177, 180))
    draw_rounded_rectangle(draw, (x + 24, y + 46, x + 126, y + 96), 6, fill=spec.accent, outline=None)
    text_center(draw, (x + 75, y + 70), spec.label.upper(), (255, 255, 255), 16)
    draw.arc((x + 12, y - 10, x + 138, y + 74), 195, 345, fill=(145, 151, 154), width=5)


def draw_brick(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 92, 96
    for offset, shade in [(0, spec.accent), (48, (217, 130, 75)), (96, (190, 94, 52))]:
        draw.polygon([(x + offset, y), (x + offset + 76, y + 18), (x + offset + 52, y + 58), (x + offset - 24, y + 40)], fill=shade, outline=(144, 70, 42))
        draw.polygon([(x + offset - 24, y + 40), (x + offset + 52, y + 58), (x + offset + 52, y + 83), (x + offset - 24, y + 65)], fill=(164, 78, 46), outline=(144, 70, 42))
    text_center(draw, (180, 176), spec.label, (84, 59, 45), 18)


def draw_helmet(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 88, 82
    shell = spec.accent
    outline = (145, 112, 73) if sum(shell) > 660 else (132, 92, 50)
    draw.pieslice((x, y - 18, x + 176, y + 140), 180, 360, fill=shell, outline=outline, width=3)
    draw.rectangle((x + 8, y + 62, x + 168, y + 88), fill=shell, outline=outline, width=3)
    draw.rectangle((x - 14, y + 82, x + 190, y + 104), fill=shell, outline=outline, width=3)
    draw.line((x + 88, y - 16, x + 88, y + 84), fill=outline, width=4)
    draw.arc((x + 34, y - 2, x + 142, y + 118), 194, 346, fill=outline, width=3)
    text_center(draw, (176, 204), spec.label, (72, 60, 45), 18)


def draw_glasses(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 65, 96
    draw_rounded_rectangle(draw, (x, y, x + 230, y + 62), 22, fill=(53, 58, 64), outline=(34, 38, 43), width=3)
    draw_rounded_rectangle(draw, (x + 16, y + 10, x + 104, y + 52), 16, fill=(213, 232, 237), outline=spec.accent, width=4)
    draw_rounded_rectangle(draw, (x + 126, y + 10, x + 214, y + 52), 16, fill=(213, 232, 237), outline=spec.accent, width=4)
    draw.rectangle((x + 104, y + 27, x + 126, y + 35), fill=(53, 58, 64))
    draw.line((x + 6, y + 12, x - 30, y - 10), fill=(61, 64, 69), width=8)
    draw.line((x + 224, y + 12, x + 260, y - 10), fill=(61, 64, 69), width=8)
    text_center(draw, (180, 188), spec.label, (56, 62, 68), 18)


def draw_roller(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 88, 72
    draw_rounded_rectangle(draw, (x, y, x + 142, y + 46), 22, fill=(236, 232, 219), outline=(178, 169, 149), width=3)
    draw.rectangle((x + 22, y + 6, x + 120, y + 40), fill=(222, 218, 205))
    draw.line((x + 142, y + 23, x + 196, y + 23), fill=(101, 112, 121), width=8)
    draw.line((x + 196, y + 23, x + 196, y + 98), fill=(101, 112, 121), width=8)
    draw_rounded_rectangle(draw, (x + 176, y + 94, x + 216, y + 156), 12, fill=spec.accent, outline=(51, 72, 92), width=3)
    text_center(draw, (180, 205), spec.label, (65, 70, 76), 18)


def draw_brush(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 102, 52
    draw_rounded_rectangle(draw, (x + 58, y + 16, x + 102, y + 160), 12, fill=spec.accent, outline=(125, 80, 42), width=3)
    draw.rectangle((x + 26, y + 130, x + 134, y + 158), fill=(196, 196, 188), outline=(126, 126, 120), width=3)
    for i in range(13):
        bx = x + 30 + i * 8
        draw.line((bx, y + 158, bx - 4, y + 204), fill=(104, 79, 51), width=5)
    text_center(draw, (180, 224), spec.label, (63, 56, 47), 18)


def draw_blades(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 72, 74
    draw_rounded_rectangle(draw, (x, y, x + 216, y + 112), 10, fill=(245, 246, 246), outline=(183, 186, 188), width=3)
    draw.rectangle((x, y, x + 216, y + 32), fill=spec.accent)
    text_center(draw, (x + 108, y + 17), spec.label.upper(), (255, 255, 255), 17)
    for i in range(5):
        yy = y + 48 + i * 10
        draw.polygon([(x + 34, yy), (x + 174, yy - 10), (x + 184, yy - 4), (x + 44, yy + 6)], fill=(181, 186, 189), outline=(132, 137, 141))
    draw.rectangle((x + 34, y + 94, x + 182, y + 100), fill=(206, 209, 211))


def draw_shield(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 100, 48
    draw_rounded_rectangle(draw, (x + 52, y, x + 108, y + 34), 8, fill=(68, 72, 77), outline=(42, 45, 49), width=3)
    draw_rounded_rectangle(draw, (x + 20, y + 26, x + 140, y + 160), 18, fill=(205, 226, 232), outline=spec.accent, width=5)
    draw.arc((x + 36, y + 44, x + 124, y + 144), 200, 340, fill=(255, 255, 255), width=4)
    draw_rounded_rectangle(draw, (x + 2, y + 154, x + 158, y + 178), 8, fill=(65, 70, 75), outline=(43, 47, 50), width=2)
    text_center(draw, (180, 224), spec.label, (55, 61, 66), 18)


def draw_knife(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 70, 105
    draw.polygon([(x, y + 18), (x + 128, y), (x + 148, y + 18), (x + 18, y + 38)], fill=(190, 196, 199), outline=(115, 121, 126))
    draw_rounded_rectangle(draw, (x + 130, y - 8, x + 248, y + 48), 14, fill=spec.accent, outline=(142, 82, 32), width=3)
    draw.rectangle((x + 162, y + 2, x + 202, y + 38), fill=(42, 48, 54))
    draw.line((x + 150, y + 13, x + 232, y + 13), fill=(241, 175, 91), width=4)
    text_center(draw, (180, 190), spec.label, (62, 58, 52), 18)


def draw_spatula(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 74, 74
    draw.polygon([(x, y + 26), (x + 132, y), (x + 150, y + 70), (x + 18, y + 94)], fill=(200, 205, 207), outline=(122, 128, 132))
    draw.line((x + 20, y + 46, x + 142, y + 22), fill=(236, 239, 240), width=4)
    draw_rounded_rectangle(draw, (x + 142, y + 20, x + 254, y + 70), 14, fill=(88, 92, 98), outline=(49, 52, 56), width=3)
    draw.rectangle((x + 162, y + 32, x + 204, y + 58), fill=(219, 162, 83))
    text_center(draw, (180, 190), spec.label, (60, 58, 54), 18)


def draw_liner(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 116, 48
    draw_rounded_rectangle(draw, (x + 12, y, x + 120, y + 170), 48, fill=spec.accent, outline=(169, 176, 184), width=3)
    draw.ellipse((x + 36, y + 30, x + 96, y + 88), fill=(248, 249, 250), outline=(169, 176, 184), width=3)
    draw.rectangle((x + 22, y + 116, x + 110, y + 178), fill=(211, 218, 224), outline=(169, 176, 184), width=3)
    text_center(draw, (180, 224), spec.label, (67, 72, 78), 17)


def draw_box(draw: ImageDraw.ImageDraw, spec: ProductImageSpec) -> None:
    x, y = 91, 82
    draw.polygon([(x, y + 36), (x + 92, y), (x + 190, y + 36), (x + 98, y + 78)], fill=(190, 143, 95), outline=(128, 92, 59))
    draw.polygon([(x, y + 36), (x + 98, y + 78), (x + 98, y + 134), (x, y + 92)], fill=spec.accent, outline=(128, 92, 59))
    draw.polygon([(x + 98, y + 78), (x + 190, y + 36), (x + 190, y + 92), (x + 98, y + 134)], fill=(134, 96, 63), outline=(128, 92, 59))
    text_center(draw, (180, 204), spec.label, (68, 56, 45), 18)


DRAWERS = {
    "bag": draw_bag,
    "bucket": draw_bucket,
    "brick": draw_brick,
    "helmet": draw_helmet,
    "glasses": draw_glasses,
    "roller": draw_roller,
    "brush": draw_brush,
    "blades": draw_blades,
    "shield": draw_shield,
    "knife": draw_knife,
    "spatula": draw_spatula,
    "liner": draw_liner,
    "box": draw_box,
}


def build_image(spec: ProductImageSpec) -> Image.Image:
    image = Image.new("RGB", (WIDTH * SCALE, HEIGHT * SCALE), (255, 255, 255))
    draw = ImageDraw.Draw(image)
    draw_floor(draw)

    drawer = DRAWERS[spec.kind]
    drawer(draw, spec)

    return fit_content(image)


def fit_content(image: Image.Image) -> Image.Image:
    background = Image.new("RGB", image.size, (255, 255, 255))
    bbox = ImageChops.difference(image, background).getbbox()
    if bbox is None:
        return image

    content = image.crop(bbox)
    max_width = WIDTH - 28
    max_height = HEIGHT - 18
    scale = min(max_width / content.width, max_height / content.height)
    target_size = (max(1, int(content.width * scale)), max(1, int(content.height * scale)))
    content = content.resize(target_size, Image.Resampling.LANCZOS)

    canvas = Image.new("RGB", (WIDTH, HEIGHT), (255, 255, 255))
    canvas.paste(content, ((WIDTH - content.width) // 2, (HEIGHT - content.height) // 2))
    return canvas


def main() -> None:
    ASSETS_DIR.mkdir(parents=True, exist_ok=True)
    for spec in SPECS:
        image = build_image(spec)
        image.save(ASSETS_DIR / f"{spec.article}.png", optimize=True)
        print(f"{spec.article}.png")


if __name__ == "__main__":
    main()
