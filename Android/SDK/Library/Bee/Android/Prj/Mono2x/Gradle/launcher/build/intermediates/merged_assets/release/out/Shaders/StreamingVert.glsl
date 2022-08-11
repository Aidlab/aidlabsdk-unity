#version 100

attribute vec3 in_position;
attribute float in_yOffset;
uniform vec4 u_color;
uniform vec3 u_modelPosition;
uniform vec3 u_modelScale;

varying vec4 varColor;

void main()
{
    varColor = u_color;

    vec3 modelCoordsPos = vec3(in_position.x * u_modelScale.x, (in_position.y + in_yOffset) * u_modelScale.y, in_position.z * u_modelScale.z);
    vec3 worldCoordsPos = modelCoordsPos + u_modelPosition;
    gl_Position = vec4(worldCoordsPos.x, clamp(worldCoordsPos.y, -1.0, 1.0), worldCoordsPos.z, 1.0);
}
