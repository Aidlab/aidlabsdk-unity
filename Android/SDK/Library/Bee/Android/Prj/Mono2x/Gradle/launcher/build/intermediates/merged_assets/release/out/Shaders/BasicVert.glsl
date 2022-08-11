#version 100

attribute vec3 in_position;

uniform vec4 u_color;
uniform vec3 u_modelPosition;
uniform vec3 u_modelScale;

varying vec4 varColor;

void main()
{
    varColor = u_color;

    vec3 position = (in_position * u_modelScale) + u_modelPosition;
    gl_Position = vec4(position, 1.0);
}
