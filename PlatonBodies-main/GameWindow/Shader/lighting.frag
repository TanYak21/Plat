#version 330 core
out vec4 FragColor;

uniform vec3 objectColor; //The color of the object
uniform vec3 lightColor; //The color of the light
uniform vec3 lightPos; //The position of the light
uniform vec3 viewPos; //The position of the view

in vec3 Normal;
in vec3 FragPos;

void main()
{
    
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);

    float diff = max(dot(norm, lightDir), 0.0); //We make sure the value is non negative with the max function
    vec3 diffuse = diff * lightColor;

    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32); //The 32 is the shininess of the material
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
}