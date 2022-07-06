#version 330 core

out vec4 FragColor;

in vec3 Normal;
in vec3 FragPos;

uniform vec3 objectColor;
uniform vec3 viewPos;

//uniform vec3 lightColor;
//uniform vec3 lightPos;

//in vec2 TexCoords;

struct DirectLight{
    vec3 lightColor;
    float ambientStre;
    float specStre;

    vec3 lightDir;
};

struct PointLight{
    vec3 lightPos;
    vec3 lightColor;
    float ambientStre;
    float specStre;

    float lightMaxDist;
};

struct SpotLight{
    vec3 lightPos;
    vec3 lightColor;
    float ambientStre;
    float specStre;

    vec3 spotDir;
    float lightMaxDist;
    float spotAngleCos;

};

# define directNumber 3
uniform DirectLight directList [directNumber];

# define pointNumber 20
uniform PointLight pointList [pointNumber];

# define spotNumber 3
uniform SpotLight spotList [spotNumber];

# define spotNumberNC 10
uniform SpotLight spotNCList [spotNumberNC];

vec3 CalcDirect(DirectLight light, vec3 norm) {
//Ambient
    vec3 ambient = light.ambientStre * light.lightColor;

    // Normalize
    //vec3 norm = normalize(normal);
    //vec3 lightDir = normalize(lightPos - FragPos);
    //vec3 spotDir = normalize(vec3(1,0,1));
    vec3 lightDir = light.lightDir;

    //Diffuse
    float diff = max(dot(norm, lightDir), 0);
    vec3 diffuse = diff * light.lightColor;
    //    diffuse = diff * lightColor;


    //Specular
    //float specularStrength = 1;
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
    vec3 specular = light.specStre * spec * light.lightColor;
    //specular = specularStrength * spec * lightColor;


    vec3 result = (ambient + diffuse + specular) * vec3(objectColor);
    return result;
}

vec3 CalcPoint(PointLight light, vec3 norm) {

    // Normalize
    vec3 normal = normalize(norm);
    vec3 lightDir = normalize(light.lightPos - FragPos);
    float dist = sqrt( pow(light.lightPos.x - FragPos.x,2) + pow(light.lightPos.y - FragPos.y,2) + pow(light.lightPos.z - FragPos.z,2) );
    
    vec3 result = vec3(0,0,0);

    if (dist < light.lightMaxDist){
        float fact_dist = max(1 - (dist/light.lightMaxDist),0);


        //Ambient

        vec3 ambient = light.ambientStre * light.lightColor * fact_dist;
        //ambient = vec3(0,0,0);

        //Diffuse
        float diff = max(dot(norm, lightDir), 0);
        vec3 diffuse = diff * light.lightColor * fact_dist;


        //Specular
        //float specularStrength = 1;
        vec3 viewDir = normalize(viewPos - FragPos);
        vec3 reflectDir = reflect(-lightDir, norm);
        float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
        vec3 specular = light.specStre * spec * light.lightColor * fact_dist;
    
        result = (ambient*0 + diffuse + specular) * vec3(objectColor);
    }


    



    
    return result;

}

vec3 CalcSpot(SpotLight light, vec3 norm) {
    // Normalize
    vec3 lightDir = normalize(light.lightPos - FragPos);
    float dist = sqrt( pow(light.lightPos.x - FragPos.x,2) + pow(light.lightPos.y - FragPos.y,2) + pow(light.lightPos.z - FragPos.z,2) );
    vec3 result = vec3(0,0,0);

    //if (dist < light.lightMaxDist){
        float fact_dist = max(1 - (dist/light.lightMaxDist),0.1f);

        //Ambient
        vec3 ambient = light.ambientStre * light.lightColor * fact_dist;

        vec3 diffuse;
        vec3 specular;
        if (dot(lightDir, light.spotDir) >= light.spotAngleCos-0.1)
        {
            float pi = 3.14159;
            float objAngle = acos(dot(normalize(lightDir), normalize(light.spotDir)));
            float innerAngle = acos(light.spotAngleCos);
            float outerAngle = innerAngle+(pi*4/180);
            //float fact_soft = clamp(1-pow(objAngle/limitAngle,5),0,1);

            float fact_soft = clamp(((cos(objAngle)-cos(outerAngle))/(cos(innerAngle)-cos(outerAngle))),0,2);
            
            //Diffuse
            float diff = max(dot(norm, lightDir), 0);
            //vec3 diffuse = diff * lightColor;
            diffuse = diff * light.lightColor * fact_dist * fact_soft;
         

            //Specular
            //float specularStrength = 1;
            vec3 viewDir = normalize(viewPos - FragPos);
            vec3 reflectDir = reflect(-lightDir, norm);
            float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
            //vec3 specular = specularStrength * spec * lightColor;
            specular = light.specStre * spec * light.lightColor * fact_dist * fact_soft;
        }
        /*else if (dot(lightDir, light.spotDir) >= light.spotAngleCos-0.03)
        {
            float softAngle = dot(lightDir, light.spotDir) - light.spotAngleCos;
            float fact_soft = max(1+softAngle/0.03f,0);
            //light.lightColor = vec3(1,0,0);
            //Diffuse
            float diff = max(dot(norm, lightDir), 0);
            //vec3 diffuse = diff * lightColor;
            diffuse = diff * light.lightColor * fact_dist * fact_soft;
         

            //Specular
            //float specularStrength = 1;
            vec3 viewDir = normalize(viewPos - FragPos);
            vec3 reflectDir = reflect(-lightDir, norm);
            float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
            //vec3 specular = specularStrength * spec * lightColor;
            specular = light.specStre * spec * light.lightColor * fact_dist * 0;
        }*/
        else
        {
            vec3 diffuse = vec3(0,0,0);
            vec3 specular = vec3(0,0,0);
        }

        result = (ambient + diffuse + specular) * vec3(objectColor);
    //}


    return result;

}

void main()
{
    vec3 norm = normalize(Normal);
    vec3 result = vec3(0,0,0);

    for(int i=0;i<directNumber;i++){
        result+=CalcDirect(directList[i], norm);
    }
    for(int i=0;i<pointNumber;i++){
        result+=CalcPoint(pointList[i], norm);
    }
    for(int i=0;i<spotNumber;i++){
        result+=CalcSpot(spotList[i], norm);
    }
    for(int i=0;i<spotNumberNC;i++){
        result+=CalcSpot(spotNCList[i], norm);
    }
    
    //vec3 result = (ambient + diffuse + specular) * objectColor;
    //vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
    //FragColor = vec4(lightColor * objectColor, 1.0);
}