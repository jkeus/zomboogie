import re
import json
import random

# Read raw input from external files
with open("./BeatHelp/raw_click_input.txt", "r") as f:
    raw_click_input = f.read()

with open("./BeatHelp/raw_dodge_input.txt", "r") as f:
    raw_dodge_input = f.read()

# Regex patterns for click and dash events
click_pattern = r"Click at time \(from beginning of song\): ([\d.]+) seconds, Position: \(([\d.-]+), ([\d.-]+)\)"
dash_pattern = r"Dash triggered at time: ([\d]+\.\d+) seconds"

# Extract time, x, y coordinates for click events
click_times_positions = [
    (float(match[0]), float(match[1]), float(match[2]))
    for match in re.findall(click_pattern, raw_click_input)
]

# Extract unique times for dash events
dash_matches = re.findall(dash_pattern, raw_dodge_input)
dash_times = sorted(set(float(time) for time in dash_matches))

# Debugging output
print("Processed dash times:", dash_times)

# Constants for event properties
smoke_duration = 1.5
idle_duration = 5.0
hit_speed = 0.5
shuriken_speed = 10
shuriken_lifetime = 3.5

# Screen boundaries
x_limit = 7.5
y_limit = 4.0

# Number of shurikens to drop per dash
num_shurikens_per_dash = 4  # Set this to your desired number

# List to store events
events = []

# Load previous map events
with open("./BeatHelp/prevClick/beat_map.json", "r") as f:
    old_map = json.load(f)

if "events" in old_map:
    events.extend(old_map["events"])

# Generate events for clicks (enemy spawns)
for time, x, y in click_times_positions:
    adjusted_spawn_time = round(time - smoke_duration, 3)
    enemy_event = {
        "spawnTime": adjusted_spawn_time,
        "prefabName": "Enemy",
        "location": {"x": x, "y": y, "z": 0},
        "enemyLifeTime": idle_duration,
        "enemySpawnSpeed": smoke_duration,
        "enemyHitSpeed": hit_speed
    }
    # Uncomment if you need to add enemy events
    # events.append(enemy_event)

# Generate events for dashes (spawning shurikens at the top at each dash time)
for time in dash_times:
    for _ in range(num_shurikens_per_dash):  # Spawn specified number of shurikens
        position = {"x": random.uniform(-x_limit, x_limit), "y": y_limit, "z": 0}  # Random position along top of screen
        shuriken_event = {
            "spawnTime": time,
            "prefabName": "Shuriken",
            "location": position,
            "shurikenLifeTime": shuriken_lifetime
        }
        events.append(shuriken_event)

# Sort events by spawn time
events.sort(key=lambda event: event["spawnTime"])

# Output to JSON
beat_map = {"events": events}

with open("./BeatHelp/beat_map.json", "w") as f:
    json.dump(beat_map, f, indent=4)

print("Beat map JSON generated successfully.")
